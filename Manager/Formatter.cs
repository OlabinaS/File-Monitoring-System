using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
	public class Formatter
	{
		public static string Parser(string fullName)
		{
			string[] spllited = new string[] { };

			if(fullName.Contains("@"))
			{
				spllited = fullName.Split('@');
				return spllited[0];
			}
			else if(fullName.Contains("\\"))
			{
				spllited = fullName.Split('\\');
				return spllited[1];
			}
			else if(fullName.Contains("CN"))
			{
				int start = fullName.IndexOf("=") + 1;
				int end = fullName.IndexOf(";");

				string str = fullName.Substring(start, end - start);

				return str;
			}

			return fullName;

		}
	}
}
