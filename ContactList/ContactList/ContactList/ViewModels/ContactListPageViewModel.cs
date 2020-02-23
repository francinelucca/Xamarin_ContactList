using ContactList.Models;
using ContactList.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ContactList.ViewModels
{
	public class ContactListPageViewModel : INotifyPropertyChanged
	{
		public const string ACTION_EDIT_CONTACT = "Edit";
		public const string ACTION_CANCEL = "Cancel";
		public const string ACTION_CALL = "Call";

		public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<Contact> Contacts { get; set; }
		
		public ICommand DeleteContactCommand { get; set; }

		public ICommand MoreOptionsCommand { get; set; }

		public ICommand AddContactCommand { get; set; }

		public ContactListPageViewModel()
		{
			this.DeleteContactCommand = new Command<Contact>((Contact contact) =>
			{
				App.Database.DeleteContact(contact.Id);
				this.Contacts.Remove(contact);
			});

			this.AddContactCommand = new Command(async() =>
			{
				await App.Current.MainPage.Navigation.PushAsync(new ContactEditPage());
			});

			this.MoreOptionsCommand = new Command<Contact>(async (Contact contact) =>
			{
				await this.ShowActionSheetWithOptions(contact);
			});
		}

		public async Task ShowActionSheetWithOptions(Contact contact)
		{
			string callContactAction = String.Format("{0} {1}", ACTION_CALL, contact.Number);
			string action = await App.Current.MainPage.DisplayActionSheet(null, ACTION_CANCEL, null, callContactAction, ACTION_EDIT_CONTACT);

			if (action == ACTION_CANCEL)
			{
				return;
			}
			else if (action == ACTION_EDIT_CONTACT)
			{
				await App.Current.MainPage.Navigation.PushAsync(new ContactEditPage(contact));
			}
			else if (action == callContactAction)
			{
				await this.OpenPhoneDialer(contact.Number);
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
		}



	}
}
