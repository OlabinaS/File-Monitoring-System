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
            string[] splitted = new string[] { };

            if (fullName.Contains("@"))//logName@domainName
            {
                splitted = fullName.Split('@');
                return splitted[0];
            }
            else if (fullName.Contains("\\"))//domainName\logName
            {
                splitted = fullName.Split('\\');
                return splitted[1];
            }
            else if (fullName.Contains("CN"))//CN=logName
            {
                int start = fullName.IndexOf("=") + 1;
                int end = fullName.IndexOf(";");
                string s = fullName.Substring(start, end - start);
                return s;
            }

            return fullName;
        }
    }
}
