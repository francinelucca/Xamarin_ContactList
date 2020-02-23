using ContactList.Models;
using ContactList.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactList.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactEditPage : ContentPage
	{
		public ContactEditPage(Contact contact = null)
		{
			InitializeComponent();
			this.BindingContext = new ContactEditPageViewModel(contact);
		}
	}
}