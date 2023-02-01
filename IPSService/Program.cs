using ServiceContracts;
using System;
using System.ServiceModel;

namespace IPSService
{
    public class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();

            string address = "net.tcp://localhost:8888/IIPSService";

            ServiceHost host = new ServiceHost(typeof(IPSService));
            host.AddServiceEndpoint(typeof(IIPSService), binding, address);

            try
            {
                host.Open();
                Console.WriteLine("IPS service started.");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("IPS service crashed");
                Console.WriteLine("Error message: {0}", e.Message);
            }
        }
    }
}
