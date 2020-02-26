using ContactList.Models;
using ContactList.Utils;
using Plugin.Media;
using System.ComponentModel;
using System.IO;
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

		public string ViewTitle { get; set; }
		
		public ICommand CreateContactCommand { get; set; }

		public ICommand PickPhotoCommand { get; set; }

		public ICommand GoBackCommand { get; set; }

		public ContactEditPageViewModel(Contact contact)
		{
			this.Contact = contact ?? new Contact();
			this.ViewTitle = contact == null ? "New Contact" : "Edit Contact";

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
						this.Contact.Color = GoogleColors.GetRandomColor();
						App.Database.SaveContact(this.Contact);
					}
					await App.Current.MainPage.Navigation.PopAsync();
				}
			});

			this.PickPhotoCommand = new Command(async () =>
			{
				await CrossMedia.Current.Initialize();

				if (!CrossMedia.Current.IsPickPhotoSupported)
				{
					await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
					return;
				}

				var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
				{
					PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
				});

				if (file == null)
					return;

				await App.Current.MainPage.DisplayAlert("File Location", file.Path, "OK");


				using(var memoryStream = new MemoryStream())
				{
					file.GetStream().CopyTo(memoryStream);
					this.Contact.Image = memoryStream.ToArray();
				}		
			});

			this.GoBackCommand = new Command(async () =>
			{
				await App.Current.MainPage.Navigation.PopAsync();
			});

		}

		public async Task<bool> Validate()
		{
			bool isValid = true;
			if (string.IsNullOrEmpty(Contact.FirstName))
			{
				await App.Current.MainPage.DisplayAlert("Name Field is Required", "Please enter a Name for your contact.", "OK");
				isValid = false;
			}
			if (string.IsNullOrEmpty(Contact.Phone))
			{
				await App.Current.MainPage.DisplayAlert("Number Field is Required", "Please enter a Number for your contact.", "OK");
				isValid = false;
			}
			else if (!Contact.Phone.IsValidPhoneNumber())
			{
				await App.Current.MainPage.DisplayAlert("Invalid Phone Number", "Please enter a valid Phone Number for you contact.", "OK");
				isValid = false;
			}

			return isValid;
		}

	}
}
