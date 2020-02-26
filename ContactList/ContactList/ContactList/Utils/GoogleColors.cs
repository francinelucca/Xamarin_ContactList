using System;
using System.Collections.Generic;
using System.Text;

namespace ContactList.Utils
{
	public static class GoogleColors
	{
		static List<string> colors = new List<String>()
		{
			"#5EB872",
			"#A95CFD",
			"#FEC832",
			"#4ECCE7",
			"#EB685A",
			"#F98F3D"
		};

		public static string GetRandomColor()
		{
			Random rnd = new Random();
			return colors[rnd.Next(0,colors.Count-1)];
		}
	}
}
