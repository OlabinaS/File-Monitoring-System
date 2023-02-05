using ServiceContracts;
using System;
using System.ServiceModel;
using Manager;
using System.Security.Principal;
using System.ServiceModel.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;

namespace IPSService
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Console.ReadLine();
            string ipsCert = Formatter.Parser(WindowsIdentity.GetCurrent().Name); //.ToUpper()//autetntifikacija putem sertifikata
           // Console.WriteLine(ipsCert);
           // Console.ReadLine();
            
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            string address = "net.tcp://localhost:8888/IIPSService";

            ServiceHost host = new ServiceHost(typeof(IPSService));
            host.AddServiceEndpoint(typeof(IIPSService), binding, address);

            host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new CertValidator_Service();

            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            host.Credentials.ServiceCertificate.Certificate = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, ipsCert);
            IPSService iPSService = null;

            ServiceSecurityAuditBehavior newAuditBehavior = new ServiceSecurityAuditBehavior();
            newAuditBehavior.AuditLogLocation = AuditLogLocation.Application;
            newAuditBehavior.ServiceAuthorizationAuditLevel = AuditLevel.SuccessOrFailure;

            host.Description.Behaviors.Remove<ServiceSecurityAuditBehavior>();
            host.Description.Behaviors.Add(newAuditBehavior);

            try
            {
                host.Open();
                Console.WriteLine("IPS service is started.\nPress <enter> to stop ...");
                iPSService = new IPSService();

                //iPSService.LogInformation(new Alarm(DateTime.Now, "a", ServiceContracts.AuditEventTypes.Information, "s"));

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
