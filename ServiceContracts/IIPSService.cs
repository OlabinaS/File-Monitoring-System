using System;
using System.ServiceModel;

namespace ServiceContracts
{
    [ServiceContract]
    public interface IIPSService
    {
        [OperationContract]
        void LogCritical();

        [OperationContract]
        void LogInformation();

        [OperationContract]
        void LogWarning();
    }
}
