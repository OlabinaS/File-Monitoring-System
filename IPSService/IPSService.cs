using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPSService
{
    public class IPSService : IIPSService, IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void LogCritical(Alarm alarm)
        {
            Console.WriteLine("Critical");
            Audit.CriticalLog(alarm);
            
        }

        public void LogInformation(Alarm alarm)
        {
            Console.WriteLine("Information");
            Audit.InformationLog(alarm);
        }


        public void LogWarning(Alarm alarm)
        {
            Console.WriteLine("Warning");
            Audit.WarningLog(alarm);
        }
    }
}
