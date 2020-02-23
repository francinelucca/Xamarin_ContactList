using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ContactList.Utils
{
	public static class StringValidator
	{

		const string ValidPhonePattern = "^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\\s\\./0-9]*$";

		public static bool IsValidPhoneNumber(this string phoneNumber)
		{
			var regex = new Regex(ValidPhonePattern, RegexOptions.IgnoreCase);
			return regex.IsMatch(phoneNumber);
		}
	}
}
