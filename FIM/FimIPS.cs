using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts;

namespace FIM
{
    public class FimIPS : ChannelFactory<IIPSService>, IIPSService, IDisposable
    {
        private IIPSService factory;

        public FimIPS(NetTcpBinding binding,EndpointAddress address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public void LogCritical(Alarm alarm)
        {
            factory.LogCritical(alarm);
        }

        public void LogInformation(Alarm alarm)
        {
            factory.LogInformation(alarm);
        }

        public void LogWarning(Alarm alarm)
        {
            factory.LogWarning(alarm);
        }
    }

}
