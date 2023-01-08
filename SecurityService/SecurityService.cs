using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityService
{
	public class SecurityService : ISecurityService
	{
		public static Dictionary<string, User> UserAccountsDB = new Dictionary<string, User>();

		/// <summary>
		/// Add new user to UserAccountsDB. Dictionary Key is "username"
		/// </summary>
		public void AddUser(string username, string password)
		{
			throw new NotImplementedException();
		}

	}
}
