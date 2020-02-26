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
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string PhoneType { get; set; }
		public string Email { get; set; }
		public string EmailType { get; set; }
		public string Company { get; set; }
		public byte[] Image { get; set; }
		public string Color { get; set; }

		public string fullName
		{
			get
			{
				return String.Format("{0} {1}", this.FirstName, this.LastName);
			}
		}

		public bool hasPicture
		{
			get
			{
				return this.Image != null;
			}
		}

		public string initialLetter
		{
			get
			{
				return this.FirstName.Substring(0, 1);
			}
		}

		public bool isGroupFirst { get; set; }
	}
}
