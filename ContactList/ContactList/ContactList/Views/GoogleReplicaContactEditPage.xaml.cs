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
	public partial class GoogleReplicaContactEditPage : ContentPage
	{
		public GoogleReplicaContactEditPage(Contact contact)
		{
			InitializeComponent();
			this.BindingContext = new ContactEditPageViewModel(contact);
		}
	}
}