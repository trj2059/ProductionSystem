/*
  Copyright 2022 Anthony johnson

  This program is free software: you can redistribute it and/or modify it under the terms of the 
  GNU Affero General Public License as published by the Free Software Foundation, either version 3 of the 
  License, or (at your option) any later version.

  This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without 
  even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See 
  the GNU Affero General Public License for more details.

  You should have received a copy of the GNU Affero General Public License along with this program. 
  If not, see <https://www.gnu.org/licenses/>.
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
        /// Same as test 2 but with a different configuration.        
        /// </summary>        
        [Fact]
        public void Operations_allApplicableProductions_Test_3()
        {
            StringBuilder cfg = new StringBuilder("aSbAs");

            var result = Operations.allApplicableProductions(prods, cfg);
            if (result is List<(string LHS, string RHS, int Index)>)
            {
                Assert.True(result.Count == 5);
            }
            else
                Assert.True(false); // TODO : What should the message be?

            if (result is List<(string LHS, string RHS, int Index)>)
            {
                Assert.Equal("S", result[0].LHS);
                Assert.Equal("Sa", result[0].RHS);

                Assert.Equal("S", result[1].LHS);
                Assert.Equal("aAb", result[1].RHS);

                Assert.Equal("b", result[2].LHS);
                Assert.Equal("aS", result[2].RHS);

                Assert.Equal("A", result[3].LHS);
                Assert.Equal("aA", result[3].RHS);

                Assert.Equal("A", result[4].LHS);
                Assert.Equal("a", result[4].RHS);
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
