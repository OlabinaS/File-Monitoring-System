using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Manager;

namespace Client
{
	public class Program
	{
		static void Main(string[] args)
		{
			NetTcpBinding binding = new NetTcpBinding();
			string address = "net.tcp://localhost:9999/FileManagerService";

			string signCertCN = Formatter.Parser(WindowsIdentity.GetCurrent().Name.ToLower()) + "_sign";
 
			using (ClientProxy proxy = new ClientProxy(binding, address))
			{
				X509Certificate2 clientCert = CertificatesManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, signCertCN);

				byte[] signature = DigitalSignature.Create("Nesto najlepse", HashAlgorithm.SHA1, clientCert);

                proxy.AddFile("file.txt", signature, "Moj najlepsi fajl");
                //proxy.ChangeFile("file.txt", signature, "Moj NAJLEPSI FAJL");
            }

			Console.ReadLine();
		}
	}
}
