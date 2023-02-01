using Manager;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FIM
{
    public class FimService : IFimSerivce
    {

        public static string path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())), "..\\Files\\"));

        public Alarm VerifySignature(string name)
        {
            //izvlaci se sertifikat iz trusted people koji sluzi za validno potpisivanje
            X509Certificate2 certificate = CertificateManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, "client_sign");

            var lines = File.ReadLines(path + name);

            string sign = lines.Last();
            string originalData = lines.First();

            try
            { 
                byte[] signature = Convert.FromBase64String(sign);
                bool check = DigitalSignature.Verify(originalData, Manager.HashAlgorithm.SHA1, signature, certificate);

                if (check == false)
                {
                    Console.WriteLine("Invalid signature");
                    return GetAlarm(name);
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid signature" + e.Message);
                return GetAlarm(name);
            }

        }

        private Alarm GetAlarm(string name)
        {
            //ovo jos ne radi
            int cnt;

            switch ()
            {
                case -1:
                    return null;

                case 0:
                    return new Alarm(DateTime.Now, path, AuditEventTypes.Information, name);

                case 1:
                    return new Alarm(DateTime.Now, path, AuditEventTypes.Warning, name);

                default:
                    return new Alarm(DateTime.Now, path, AuditEventTypes.Critical, name);
            }
        }


    }
}
