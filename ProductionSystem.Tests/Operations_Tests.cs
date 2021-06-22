using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionSystem.ProductionOperations;
using Xunit;

#nullable enable

namespace ProductionSystem.Tests
{
    public class Operations_Tests
    {
        private List<List<(string LHS, string RHS)>> prods { get; set; }

        private StringBuilder? config { get; set; }

        public Operations_Tests()
        {
            prods = new List<List<(string LHS, string RHS)>>();
            prods.Add(new List<(string LHS, string RHS)>() { ("S", "Sa") });
            prods.Add(new List<(string LHS, string RHS)>() { ("S", "aAb") });
            prods.Add(new List<(string LHS, string RHS)>() { ("A", "aA") });
            prods.Add(new List<(string LHS, string RHS)>() { ("A", "a") });
            config = new StringBuilder("S");
        }

        [Fact]
        public void Operations_Test1()
        {
			var result = Test1_Helper();
			Assert.NotNull(result);
        }

        private StringBuilder? Test1_Helper()
        {
			try
			{
				if (config is StringBuilder)
				{
					Console.WriteLine(config.ToString());
					var canApply = Operations.canApplyProd(prods[0][0], config, 0);
					if (canApply is bool)
					{
						if ((bool)canApply)
						{
							config = Operations.applyProd(prods[0][0], config, 0);
							if (config is StringBuilder)
								return config;
						}
						else
                        {
							throw new Exception("Cannot apply production");
						}
					}
					else
					{
						throw new Exception("Cannot apply production");
					}
				}
				else
				{
					throw new Exception("config is null");
				}
				return null;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return null;
			}
		}
    }
}
