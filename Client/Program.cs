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

			string signCertCN = "";

			WindowsIdentity identity = WindowsIdentity.GetCurrent();
			WindowsPrincipal principal = new WindowsPrincipal(identity);

			//postoje 2 grupe korisnika valid i nonvalid
			foreach(var group in identity.Groups)
			{
                var account = (NTAccount)group.Translate(typeof(NTAccount));

				string[] gr = Convert.ToString(account.Value).Split('\\');
				//nekiKod\ImeGrupe
				if (gr.Count() > 1)
                {
					if (gr[1] == "Valid" || gr[1] == "Nonvalid")
					{
						//ovde dodelimo vrednost subjectName-a na osnovu grupe kojoj korisnik pripada
						signCertCN = gr[1].ToLower() + "_sign";
						break;
					}
					else
					{
						signCertCN = "NonExistingCert";
					}
                }
				//signCertCN = "valid_sign";

			}
			Console.WriteLine(signCertCN);
			using (ClientProxy proxy = new ClientProxy(binding, address))
			{
				X509Certificate2 Cert = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, signCertCN);
				if(Cert == null)
                { Console.WriteLine("ALOO"); }
				byte[] signature;

				while(true)
				{
					Console.WriteLine("---------------------------------------------");
					Console.WriteLine("1.Add new file\n2.Change file\n0.Exit!");
					Console.Write("Enter an option: ");

					string optionStr = Console.ReadLine();
					int option;
					if(!int.TryParse(optionStr, out option))
					{
						continue;
					}

					switch(option)
					{
						case 1:
							Console.WriteLine("Enter a name of file: ");
							string filename = Console.ReadLine();
							Console.WriteLine("Enter a text: ");
							string text = Console.ReadLine();

							signature = DigitalSignature.Create(text, HashAlgorithm.SHA1, Cert);
							proxy.AddFile(filename, signature, text);
							break;

						case 2:
							Console.WriteLine("Enter a name of file: ");
							filename = Console.ReadLine();
							Console.WriteLine("Enter a text: ");
							text = Console.ReadLine();

							signature = DigitalSignature.Create(text, HashAlgorithm.SHA1, Cert);
							proxy.ChangeFile(filename, signature, text);
							break;

						case 0:
							Console.WriteLine("Press any key to exit");
							Console.ReadKey();
							break;

						default:
							Console.WriteLine("Enter the option number!!");
							break;
					}

					if (option == 0)
						break;

				}

                //proxy.AddFile("file.txt", "Moj najlepsi fajl");
                //proxy.ChangeFile("file.txt", "Moj NAJLEPSI FAJL");
            }

			//Console.ReadLine();
		}
	}
}
