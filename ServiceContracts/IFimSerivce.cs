﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    [ServiceContract]
    public interface IFimSerivce
    {
        [OperationContract]
        Alarm VerifySignature(string name);
    }
}
