using System;

namespace ProductionSystem.Exceptions
{
    /// <summary>
    /// We use a base class so that we can add the stack in the listing of the
    /// production used in any derived class.
    /// </summary>
    public abstract class AbstractProductionBaseException : Exception
    {
        public AbstractProductionBaseException(string message)
            : base(message + "\n\n" + Environment.StackTrace) { }
    }
}
