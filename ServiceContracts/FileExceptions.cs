using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    [DataContract]
    public class FileExceptions
    {
        [DataMember]
        public string Message { get; set; } 
        
        public FileExceptions(string message)
        {
            Message = message;
        }

    }
}
