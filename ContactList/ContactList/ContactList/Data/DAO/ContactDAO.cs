using ContactList.Models;
using PCLStorage;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactList.Data.DAO
{
	public class ContactDAO
	{
		static object locker = new object();
		SQLiteConnection database;

		public ContactDAO()
		{
			database = GetConnection();
			database.CreateTable<Contact>();
		}
		public SQLiteConnection GetConnection()
		{
			SQLiteConnection sqlitConnection;
			var sqliteFilename = "Contactv2.db";
			IFolder folder = FileSystem.Current.LocalStorage;
			string path = PortablePath.Combine(folder.Path.ToString(), sqliteFilename);
			sqlitConnection = new SQLiteConnection(path);
			return sqlitConnection;
		}

		public IEnumerable<Contact> GetContacts()
		{
			lock (locker)
			{
				return (from i in database.Table<Contact>() select i).ToList();
			}
		}

		public Contact GetContact(int id)
		{
			return database.Table<Contact>().FirstOrDefault(c => c.Id == id);
		}

		public int SaveContact(Contact contact)
		{
			lock (locker)
			{
				return database.Insert(contact);
			}
		}
		public int UpdateContact(Contact contact)
		{
			lock (locker)
			{
				database.Update(contact);
				return contact.Id;
			}
		}
		public int DeleteContact(int id)
		{
			lock (locker)
			{
				return database.Delete<Contact>(id);
			}
		}

		public IEnumerable<Contact> FilterContacts(string searchTerm)
		{
			return this.GetContacts().Where(c => c.fullName.Contains(searchTerm));
		}
	}
}
