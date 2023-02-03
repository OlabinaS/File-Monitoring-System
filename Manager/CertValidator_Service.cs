using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
	public class CertValidator_Service : X509CertificateValidator
	{
		public override void Validate(X509Certificate2 certificate)
		{
			X509Certificate2 Cert = CertificateManager.GetCertificateFromStorage
				(StoreName.My, StoreLocation.LocalMachine, Formatter.Parser(WindowsIdentity.GetCurrent().Name));

			if (!certificate.Issuer.Equals(Cert.Issuer))
			{
				throw new Exception("Certificate is NOT valid!!");
			}
		}
	}
}
