using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
	public class Program
	{
		static void Main(string[] args)
		{
			NetTcpBinding binding = new NetTcpBinding();
			string address = "net.tcp://localhost:9999/FileManagerService";
 
			using (ClientProxy proxy = new ClientProxy(binding, address))
			{

			}

			Console.ReadLine();
		}
	}
}
