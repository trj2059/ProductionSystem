using System;
using System.Collections.Generic;
using System.Text;

namespace ProductionSystem.Exceptions
{
    public class IndexGreaterThanCfgLengthException : AbstractProductionBaseException
    {
        public IndexGreaterThanCfgLengthException() : base("Index greater than cfg length.")
        {

        }
    }
}
