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
using ZXing.Net.Mobile.Forms;

namespace ContactList.ViewModels
{
	public class ContactListPageViewModel : INotifyPropertyChanged
	{
		public const string ActionEditContact = "Edit";
		public const string ActionCancel = "Cancel";
		public const string ActionCallContact = "Call";

		public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<Contact> Contacts { get; set; }
		public List<ObservableGroupCollection<string, Contact>> GroupedContacts { get; set; }

		public ICommand DeleteContactCommand { get; set; }

		public ICommand MoreOptionsCommand { get; set; }

		public ICommand AddContactCommand { get; set; }

		public ICommand OpenScannerCommand { get; set; }

		public string SearchTerm { get; set; }

		private bool UseGoogleUI { get; set; }

		public ContactListPageViewModel(bool useGoogleUI = false)
		{
			this.UseGoogleUI = useGoogleUI;
			this.DeleteContactCommand = new Command<Contact>((Contact contact) =>
			{
				App.Database.DeleteContact(contact.Id);
				this.Contacts.Remove(contact);
				if (useGoogleUI)
				{
					GroupContacts();
				}
			});

			this.AddContactCommand = new Command(async() =>
			{
				if (useGoogleUI)
				{
				 await App.Current.MainPage.Navigation.PushAsync(new GoogleReplicaContactEditPage(null));
				}
				else
				{
					await App.Current.MainPage.Navigation.PushAsync(new ContactEditPage(null));
				}
			});

			this.MoreOptionsCommand = new Command<Contact>(async (Contact contact) =>
			{
				await this.ShowActionSheetWithOptions(contact);
			});

			this.OpenScannerCommand = new Command(() =>
			{
				this.OpenScanner();
			});
		}

		public async Task ShowActionSheetWithOptions(Contact contact)
		{
			string callContactAction = String.Format("{0} {1}", ActionCallContact, contact.Phone);
			string action = await App.Current.MainPage.DisplayActionSheet(null, ActionCancel, null, callContactAction, ActionEditContact);

			if (action == ActionCancel)
			{
				return;
			}
			else if (action == ActionEditContact)
			{
				if (UseGoogleUI)
				{
					await App.Current.MainPage.Navigation.PushAsync(new GoogleReplicaContactEditPage(contact));
				}
				else
				{
					await App.Current.MainPage.Navigation.PushAsync(new ContactEditPage(contact));
				}
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
			if (this.UseGoogleUI)
			{
				GroupContacts();
			}
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

		public async void OpenScanner()
		{

			var ScannerPage = new ZXingScannerPage();
			ScannerPage.IsScanning = true;

			ScannerPage.OnScanResult += (result) => {

				ScannerPage.IsScanning = false;

				Device.BeginInvokeOnMainThread(async () => {
					string[] values = result.Text.Split('*');
					if (values.Length == 3)
					{
						Contact contact = new Contact();
						contact.FirstName = values[0];
						contact.LastName = values[1];
						contact.Phone = values[2];
						contact.Color = GoogleColors.GetRandomColor();
						App.Database.SaveContact(contact);
					}
					else
					{
						await App.Current.MainPage.DisplayAlert("Invalid QR Code Scanned", "Couldn't load contact data from scanned QR Code", "OK");

					}
					await App.Current.MainPage.Navigation.PopAsync();
				});
			};


			await App.Current.MainPage.Navigation.PushAsync(ScannerPage);
		}
	}
}
