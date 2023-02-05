using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    [ServiceContract]
    public interface IFileManagerServiceRemove
    {
        [OperationContract]
        [FaultContract(typeof(FileExceptions))]
        void RemoveFile(string name);
    }
}
