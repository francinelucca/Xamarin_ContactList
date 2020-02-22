using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ContactList.Models
{
	public class Contact : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public string Name { get; set; }
		public string Number { get; set; }

	}
}
