using System;
using System.Collections.Generic;
using System.Text;

namespace ProductionSystem.Exceptions
{
    /// <summary>
    /// Configuration is null exception.
    /// </summary>
    public class CfgIsNullException : AbstractProductionBaseException
    {
        public CfgIsNullException()
            : base("The configuration is null is null.")
        {

        }
    }
}
