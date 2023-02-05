using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Manager;
using ServiceContracts;

namespace FIM
{
    public class FimIPS : ChannelFactory<IIPSService>, IIPSService, IDisposable
    {
        private IIPSService factory;

        public FimIPS(NetTcpBinding binding,EndpointAddress address) : base(binding, address)
        {
            //uzimamo ime od onog koji pokrece da nam sluzi kao subject
            string userCertCN = Formatter.Parser(WindowsIdentity.GetCurrent().Name);

            //custom provera serifikata
            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new CertValidator_Client();
            
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            
            //dobavljamo sertifikat sa subject userCert
            this.Credentials.ClientCertificate.Certificate = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, userCertCN);
            
            factory = this.CreateChannel();
        }

        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }

            this.Close();
        }

        public void LogCritical(Alarm alarm)
        {
            try
            {
                factory.LogCritical(alarm);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Critical log error: {0}", ex.Message);
            }
            
        }

        public void LogInformation(Alarm alarm)
        {
            try
            {
                factory.LogInformation(alarm);
            }
            catch(Exception ex) { 
                Console.WriteLine("Information log error: {0}", ex.Message);
            }


        }

        public void LogWarning(Alarm alarm)
        {
            try
            {
                factory.LogWarning(alarm);
            }
            catch(Exception ex) {
                Console.WriteLine("Warning log error: {0}", ex.Message);
            }

        }

    }

}
