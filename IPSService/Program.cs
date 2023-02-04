using ServiceContracts;
using System;
using System.ServiceModel;
using Manager;
using System.Security.Principal;
using System.ServiceModel.Security;
using System.Security.Cryptography.X509Certificates;

namespace IPSService
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Console.ReadLine();
            string ipsCert = Formatter.Parser(WindowsIdentity.GetCurrent().Name.ToUpper()); //autetntifikacija putem sertifikata
           // Console.WriteLine(ipsCert);
           // Console.ReadLine();
            
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate; //autetntifikacija putem sertifikata

            string address = "net.tcp://localhost:8888/IIPSService";

            ServiceHost host = new ServiceHost(typeof(IPSService));
            host.AddServiceEndpoint(typeof(IIPSService), binding, address);

            host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom; //autetntifikacija putem sertifikata
            host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new CertValidator_Service(); //autetntifikacija putem sertifikata

            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            host.Credentials.ServiceCertificate.Certificate = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, ipsCert); //autetntifikacija putem sertifikata
            IPSService iPSService = null;

            try
            {
                host.Open();
                Console.WriteLine("IPS service is started.\nPress <enter> to stop ...");
                iPSService = new IPSService();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong while starting IPS service\nError: {e.Message}");
                Console.WriteLine(e.StackTrace);
                Console.ReadLine();

            }
            finally
            {
                iPSService.Dispose();
                host.Close();
            }
        }
    }
}
