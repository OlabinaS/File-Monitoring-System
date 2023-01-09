using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
	[ServiceContract]
	public interface IFileManagerServiceAddChange
	{
		[OperationContract]
        [FaultContract(typeof(FileExceptions))]
        void AddFile(string name, string text);

		[OperationContract]
        [FaultContract(typeof(FileExceptions))]
        void ChangeFile(string name, string text);

	}
}
