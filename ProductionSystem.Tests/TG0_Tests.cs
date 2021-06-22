using System;
using ProductionSystem;
using Xunit;

namespace ProductionSystem.Tests
{
    public class TG0_Tests
    {
        private T0G t0G = null;

        public TG0_Tests()
        {
            t0G = new T0G();
        }

        [Fact]
        public void Test1()
        {
            var ret_value = t0G.Test1();
            Assert.NotNull(ret_value);
        }
    }
}
