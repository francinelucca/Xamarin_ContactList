using ContactList.Data.DAO;
using ContactList.Views;
using Xamarin.Forms;

namespace ContactList
{
	public partial class App : Application
	{
		static ContactDAO database;

		public static ContactDAO Database
		{
			get
			{
				if(database == null)
				{
					database = new ContactDAO();
				}
				return database;
			}
		}
		public App()
		{
			InitializeComponent();
			MainPage = new NavigationPage(new GoogleReplicaContactListPage());
		//	MainPage = new NavigationPage(new ContactListPage());
		}
		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
