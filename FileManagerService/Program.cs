using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.ServiceModel;
using ServiceContracts;

namespace FileManagerService
{
	class Program
	{
		static void Main(string[] args)
		{
			NetTcpBinding binding = new NetTcpBinding();

			binding.Security.Mode = SecurityMode.Transport;
			binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
			binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

			string address = "net.tcp://localhost:9999/FileManagerService";

			ServiceHost host = new ServiceHost(typeof(FileManager));
			host.AddServiceEndpoint(typeof(IFileManagerService), binding, address);

			host.Open();

			Console.WriteLine("Working...");

			Console.ReadLine();
		}
	}
}
