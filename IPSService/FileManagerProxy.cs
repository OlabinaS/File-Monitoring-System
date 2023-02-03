using ServiceContracts;
using System;
using System.ServiceModel;

namespace IPSService
{
    public class FileManagerProxy : ChannelFactory<IFileManagerServiceRemove>, IFileManagerServiceRemove, IDisposable
    {
        private IFileManagerServiceRemove factory;

        public FileManagerProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public FileManagerProxy(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public void RemoveFile(string name)
        {
            try
            {
                factory.RemoveFile(name);
            }
            catch (Exception e) when (e is FaultException<FileExceptions> || e is FaultException<FileOperationsException>)
            {
                Console.WriteLine($"{e.Detail.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
