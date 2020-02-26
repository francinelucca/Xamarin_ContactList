using ContactList.Models;
using ContactList.Utils;
using ContactList.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ContactList.ViewModels
{
	public class GoogleReplicaContactListPageViewModel : INotifyPropertyChanged
	{
		public const string ACTION_EDIT_CONTACT = "Edit";
		public const string ACTION_CANCEL = "Cancel";
		public const string ACTION_CALL = "Call";

		public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<Contact> Contacts { get; set; }
		public List<ObservableGroupCollection<string, Contact>> GroupedContacts { get; set; }

		public ICommand DeleteContactCommand { get; set; }

		public ICommand MoreOptionsCommand { get; set; }

		public ICommand AddContactCommand { get; set; }

		public String SearchTerm { get; set; }

		public GoogleReplicaContactListPageViewModel()
		{
			this.DeleteContactCommand = new Command<Contact>((Contact contact) =>
			{
				App.Database.DeleteContact(contact.Id);
				this.Contacts.Remove(contact);
				GroupContacts();
			});

			this.AddContactCommand = new Command(async () =>
			{
				await App.Current.MainPage.Navigation.PushAsync(new GoogleReplicaContactEditPage(null));
			});

			this.MoreOptionsCommand = new Command<Contact>(async (Contact contact) =>
			{
				await this.ShowActionSheetWithOptions(contact);
			});
		}

		public async Task ShowActionSheetWithOptions(Contact contact)
		{
			string callContactAction = String.Format("{0} {1}", ACTION_CALL, contact.Phone);
			string action = await App.Current.MainPage.DisplayActionSheet(null, ACTION_CANCEL, null, callContactAction, ACTION_EDIT_CONTACT);

			if (action == ACTION_CANCEL)
			{
				return;
			}
			else if (action == ACTION_EDIT_CONTACT)
			{
				await App.Current.MainPage.Navigation.PushAsync(new GoogleReplicaContactEditPage(contact));
			}
			else if (action == callContactAction)
			{
				await this.OpenPhoneDialer(contact.Phone);
			}
		}

		public async Task OpenPhoneDialer(string contactNumber)
		{

			try
			{
				PhoneDialer.Open(contactNumber);
			}
			catch (ArgumentNullException anEx)
			{
				await App.Current.MainPage.DisplayAlert("Error Occurred",
														"Please verify that the contact number is a valid phone number",
														"Ok");
			}
			catch (FeatureNotSupportedException ex)
			{
				await App.Current.MainPage.DisplayAlert("Error Occurred",
														"Cannot Open Phone Dialer on this device, please dial number manually",
														"Ok");
			}
			catch (Exception ex)
			{
				await App.Current.MainPage.DisplayAlert("Error Occurred",
														"Unexpected Error, Please contact administrator",
														"Ok");
			}
		}

		public void RefreshContactsData()
		{
			this.GetContacts();
		}
		protected void GetContacts()
		{
			this.Contacts = new ObservableCollection<Contact>(App.Database.GetContacts());
			GroupContacts();
		}

		public void SearchContacts()
		{
			this.Contacts = new ObservableCollection<Contact>(App.Database.FilterContacts(this.SearchTerm));
			GroupContacts();
		}

		protected void GroupContacts()
		{
			this.GroupedContacts = this.Contacts
									.OrderBy(c => c.fullName)
									.GroupBy(c => c.FirstName[0].ToString())
									.Select(c => new ObservableGroupCollection<string, Contact>(c)).ToList();

			this.GroupedContacts.ForEach(g => g.ToList().First().isGroupFirst = true); ;
		}



	}
}
