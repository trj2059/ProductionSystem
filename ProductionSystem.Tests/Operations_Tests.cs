using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private List<(string LHS, string RHS)> prods { get; set; }

        private StringBuilder? config { get; set; }

        public Operations_Tests()
        {
            // TODO : Something is wrong here...
            // We need to place some restrictions on prductions..
            // the List of Lists may not work.  
            // I think we can get away with on list of productions.
            // ...
            // 
            prods = new List<(string LHS, string RHS)>();
            prods.Add(("S", "Sa"));
            prods.Add(("S", "aAb"));
            prods.Add(("b", "aS"));
            prods.Add(("A", "aA"));
            prods.Add(("A", "a"));
            config = new StringBuilder("S");
        }

        [Fact]
        public void Operations_Test1()
        {
            var result = Test1_Helper();
            Assert.NotNull(result);
        }

        [Fact]
        public void Operations_getListOfApplicableIndices_Test_1()
        {
            StringBuilder cfg = new StringBuilder("aSA");
            var result = Operations.getListOfApplicableIndices(prods[0], cfg);
            if (result is List<int>)
            {
                Assert.True(result.Count == 1);
            }
            else
                Assert.True(false); // TODO : What should the message be?

            if (result is List<int>)
            {
                Assert.True(result[0] == 1);
            }
            else
                Assert.True(false); // TODO : What should the message be?

        }

        [Fact]
        public void Operations_allApplicableProductions_Test_2()
        {
            StringBuilder cfg = new StringBuilder("Sb");
            var result = Operations.allApplicableProductions(prods, cfg);
            if (result is List<(string LHS, string RHS, int Index)>)
            {
                Assert.True(result.Count == 2);
            }
            else
                Assert.True(false); // TODO : What should the message be?

            if (result is List<(string LHS, string RHS, int Index)>)
            {
                Assert.Equal("S", result[0].LHS);
                Assert.Equal("Sa", result[0].RHS);

                Assert.Equal("S", result[1].LHS);
                Assert.Equal("aAb", result[1].RHS);
            }
            else
                Assert.True(false); // TODO : What should the message be?

        }


        private StringBuilder? Test1_Helper()
        {
            try
            {
                if (config is StringBuilder)
                {
                    Debug.WriteLine(config.ToString());
                    var canApply = Operations.canApplyProd(prods[0], config, 0);
                    if (canApply is bool)
                    {
                        if ((bool)canApply)
                        {
                            config = Operations.applyProd(prods[0], config, 0);
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
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
