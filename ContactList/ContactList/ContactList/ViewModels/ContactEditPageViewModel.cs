using ContactList.Models;
using ContactList.Utils;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactList.ViewModels
{
	public class ContactEditPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public Contact Contact { get; set; }
		
		public ICommand CreateContactCommand { get; set; }

		public ContactEditPageViewModel(Contact contact)
		{
			this.Contact = contact ?? new Contact();

			this.CreateContactCommand = new Command(async() =>
			{
				if(await Validate())
				{
					if (this.Contact.Id != 0)
					{
						App.Database.UpdateContact(this.Contact);
					}
					else
					{
						App.Database.SaveContact(this.Contact);
					}
					await App.Current.MainPage.Navigation.PopAsync();
				}
			});

		}

		public async Task<bool> Validate()
		{
			bool isValid = true;
			if (string.IsNullOrEmpty(Contact.Name))
			{
				await App.Current.MainPage.DisplayAlert("Name Field is Required", "Please enter a Name for your contact.", "OK");
				isValid = false;
			}
			if (string.IsNullOrEmpty(Contact.Number))
			{
				await App.Current.MainPage.DisplayAlert("Number Field is Required", "Please enter a Number for your contact.", "OK");
				isValid = false;
			}
			else if (!Contact.Number.IsValidPhoneNumber())
			{
				await App.Current.MainPage.DisplayAlert("Invalid Phone Number", "Please enter a valid Phone Number for you contact.", "OK");
				isValid = false;
			}

			return isValid;
		}

	}
}
