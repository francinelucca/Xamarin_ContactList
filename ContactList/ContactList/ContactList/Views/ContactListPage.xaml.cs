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
	public partial class ContactListPage : ContentPage
	{
		private ContactListPageViewModel Vm;
		public ContactListPage()
		{
			InitializeComponent();
			this.BindingContext = this.Vm = new ContactListPageViewModel();
		}
		protected async override void OnAppearing()
		{
			base.OnAppearing();
			this.Vm.RefreshContactsData();
		}
	}
}