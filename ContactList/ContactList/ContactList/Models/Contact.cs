using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;

namespace ContactList.Models
{
	public class Contact : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Number { get; set; }

	}
}
