using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
	public class CertValidator_Client : X509CertificateValidator
	{
		public override void Validate(X509Certificate2 certificate)
		{
			if(certificate.Subject.Equals(certificate.Issuer))
			{
				throw new Exception("Cartificate is NOT valid!!");
			}
		}
	}
}
