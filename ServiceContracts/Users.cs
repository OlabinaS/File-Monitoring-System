using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
	public class User
	{		
		string username = string.Empty;
		string password = string.Empty;

		public User(string _username, string _password)
		{
			this.username = _username;
			this.password = _password;
		}		

		public string Username
		{
			get { return username; }
			set { username = value; }
		}

		public string Password
		{
			get { return password; }
			set { password = value; }
		}
       
	}
}
