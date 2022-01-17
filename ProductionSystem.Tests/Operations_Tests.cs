/*
  Copyright 2022 Anthony johnson

  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files
  (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, 
  merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is 
  furnished to do so, subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
  OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE 
  LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR 
  IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

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
        /// <summary>
        /// The list of productions, where LHS stands for "Left Hand Side", and
        /// RHS stands for "Right Hand Side"
        /// Productions are, for example of the form:
        /// 
        ///  S -> aSa
        ///  
        /// </summary>
        private List<(string LHS, string RHS)> prods { get; set; }

        /// <summary>
        /// TODO: find the Automata book that talks about the configuration
        ///       the wikipedia article doesn't explain this
        /// </summary>
        private StringBuilder? config { get; set; }

        /// <summary>
        /// Initalize the production set, and the current configuration.
        /// See the wikipedia article to make sense of this
        /// https://en.wikipedia.org/wiki/Production_(computer_science)
        /// </summary>
        public Operations_Tests()
        {
            
            prods = new List<(string LHS, string RHS)>();
            prods.Add(("S", "Sa"));
            prods.Add(("S", "aAb"));
            prods.Add(("b", "aS"));
            prods.Add(("A", "aA"));
            prods.Add(("A", "a"));
            config = new StringBuilder("S");
        }

        /// <summary>
        /// Simple test to see if some basic functionality is working.
        /// This code should never break.
        /// TODO: Give this a better name.
        /// </summary>
        [Fact]
        public void Operations_Test1()
        {
            var result = Test1_Helper();
            Assert.NotNull(result);
        }
        
        /// <summary>
        /// Testing to see if we get the correct indicies where
        /// we can apply the productions
        /// </summary>
        [Fact]
        public void Operations_getListOfApplicableIndices_Test_1()
        {
            StringBuilder cfg = new StringBuilder("aSA");
            var result = Operations.getListOfApplicableIndices(prods[0], cfg);
            if (result is List<int>)
            {
                // there is only one applicable production.
                Assert.True(result.Count == 1);
            }
            else
                Assert.True(false); // TODO : What should the message be?

            if (result is List<int>)
            {
                // here we verify that the indici is correct.
                Assert.True(result[0] == 1);
            }
            else
                Assert.True(false); // TODO : What should the message be?

        }

        /// <summary>
        /// This test is the same as test1, except we test to be sure the
        /// results are accurate
        /// </summary>
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

        /// <summary>
        /// TODO: Rename this something appropriate
        /// </summary>        
        private StringBuilder? Test1_Helper()
        {
            try
            {
                if (config is StringBuilder)
                {
                    Debug.WriteLine(config.ToString());
                    // Can we apply production 0 to the current configuration at index 0.
                    var canApply = Operations.canApplyProd(prods[0], config, 0);
                    if (canApply is bool)
                    {
                        // if so apply the production and return the new updated configuration
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
