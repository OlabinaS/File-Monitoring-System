using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPSService
{
    public class IPSService : IIPSService
    {
        public void LogCritical(Alarm alarm)
        {
            throw new NotImplementedException();
        }

        public void LogInformation(Alarm alarm)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(Alarm alarm)
        {
            throw new NotImplementedException();
        }
    }
}
