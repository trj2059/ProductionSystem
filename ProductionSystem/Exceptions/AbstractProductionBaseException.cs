using System;

namespace ProductionSystem.Exceptions
{
    public abstract class AbstractProductionBaseException : Exception
    {
        public AbstractProductionBaseException(string message)
            : base(message + "\n\n" + Environment.StackTrace) { }
    }
}
