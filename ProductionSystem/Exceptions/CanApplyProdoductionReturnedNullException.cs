using System;
using System.Collections.Generic;
using System.Text;

namespace ProductionSystem.Exceptions
{
    /// <summary>    
    /// </summary>
    public class CanApplyProdoductionReturnedNullException : AbstractProductionBaseException
    {
        public CanApplyProdoductionReturnedNullException()
            : base("canApplyProdduction returned null")
        {

        }
    }
}
