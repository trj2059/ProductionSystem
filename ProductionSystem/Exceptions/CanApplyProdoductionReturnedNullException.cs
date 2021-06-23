using System;
using System.Collections.Generic;
using System.Text;

namespace ProductionSystem.Exceptions
{
    public class CanApplyProdoductionReturnedNullException : AbstractProductionBaseException
    {
        public CanApplyProdoductionReturnedNullException()
            : base("canApplyProd returned null")
        {

        }
    }
}
