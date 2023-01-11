using System.ServiceModel;

namespace ServiceContracts
{
    [ServiceContract]
    public interface IIPSService
    {
        [OperationContract]
        void LogCritical(Alarm alarm);

        [OperationContract]
        void LogInformation(Alarm alarm);

        [OperationContract]
        void LogWarning(Alarm alarm);
    }
}
