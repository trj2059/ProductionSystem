using System;
using System.Collections.Generic;
using System.Text;

namespace ProductionSystem.Exceptions
{
    public class CfgIsNullException : AbstractProductionBaseException
    {
        public CfgIsNullException()
            : base("Cfg is null.")
        {

        }
    }
}
