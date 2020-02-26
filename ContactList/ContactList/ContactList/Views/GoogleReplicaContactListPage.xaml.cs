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
	public partial class GoogleReplicaContactListPage : ContentPage
	{
		private GoogleReplicaContactListPageViewModel Vm;
		public GoogleReplicaContactListPage()
		{
			InitializeComponent();
			this.BindingContext = this.Vm = new GoogleReplicaContactListPageViewModel();
		}
		protected async override void OnAppearing()
		{
			base.OnAppearing();
			this.Vm.RefreshContactsData();
		}

		private void ContactsSearch(object sender, TextChangedEventArgs e)
		{
			this.Vm.SearchContacts();
		}
	}
}