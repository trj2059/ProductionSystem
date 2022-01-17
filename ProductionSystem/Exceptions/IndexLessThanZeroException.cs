using System;
using System.Collections.Generic;
using System.Text;

namespace ProductionSystem.Exceptions
{
    public class IndexLessThanZeroException : AbstractProductionBaseException
    {
        public IndexLessThanZeroException() : base("Index less than 0.")
        {

        }
    }
}
