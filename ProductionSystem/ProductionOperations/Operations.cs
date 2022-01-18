﻿/*
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
using System.Text;
using ProductionSystem.Exceptions;

#nullable enable

namespace ProductionSystem.ProductionOperations
{
    public static class Operations
    {
        /// <summary>
        /// Determines if a given production can be applied to a configuration.
        /// </summary>
        /// <param name="prod">A given production</param>
        /// <param name="cfg">The current grammar configuraton</param>
        /// <param name="index">The index of the symbol on the grammar to be substituted</param>
        /// <returns>Returns true if the production can be applied to the configuation, false otherwise.</returns>
        public static bool? canApplyProd((string LHS, string RHS) prod, StringBuilder cfg, int index)
        {
            if (index < 0)
                throw new IndexLessThanZeroException();
            if (index > cfg.Length - 1)
                throw new IndexGreaterThanCfgLengthException();

            Char[] cfgCharArrray = cfg.ToString().ToCharArray();
            int i = index;
            foreach (var c in prod.LHS)
            {
                if (cfgCharArrray[i] != c)
                    return false;
                i += 1;
            }

            return true;
        }

        /// <summary>
        /// Apply a given production to an indicated substring of a configuation
        /// </summary>
        /// <param name="prod">A given production</param>
        /// <param name="cfg">The current grammar configuraton</param>
        /// <param name="index">The index of the symbol on the grammar to be substituted</param>
        /// <returns>
        /// Returns the transformed configuration after applying the production, or returns null if
        /// there was an exception
        /// </returns>
        public static StringBuilder? applyProd((string LHS, string RHS) prod, StringBuilder? cfg, int index)
        {
            if (cfg is StringBuilder)
            {
                cfg.Remove(index, prod.LHS.Length);
                cfg.Insert(index, prod.RHS);
                return cfg;
            }
            else
                throw new CfgIsNullException();
        }

        /// <summary>
        /// Finds the list of all indicies you can apply a given production to a cfg
        /// Attemps to apply a production's LHS to all possible indices of a cfg
        /// up to the cfg.Length - lhs.length.
        /// </summary>
        /// <param name="prod">The  production to be used in the test</param>
        /// <param name="cfg">The configuration that is to be tested against</param>
        /// <returns>A list of all applicable indicies the lhs can be applied to the cfg</returns>
        public static List<int>? getListOfApplicableIndices((string LHS, string RHS) prod, StringBuilder? cfg)
        {
            var ret_value = new List<int>();

            if (cfg is StringBuilder)
            {
                for (int i = 0; i < cfg.Length - prod.LHS.Length; i++)
                {
                    bool? canApply = canApplyProd(prod, cfg, i);
                    if (canApply is bool)
                    {
                        if ((bool)canApply)
                            ret_value.Add(i);
                    }
                    else
                        throw new CanApplyProdoductionReturnedNullException();
                }
            }
            else
                throw new CfgIsNullException();

            return ret_value;
        }

        /// <summary>
        /// Determins what productions can be applied to a given configuation
        /// </summary>
        /// <param name="prods">A list of productions</param>
        /// <param name="cfg">A grammar configuration</param>
        /// <returns>All applicable productions.  Returns null if there is an exception.</returns>
        public static List<(string LHS, string RHS, int Index)>? allApplicableProductions(List<(string LHS, string RHS)> prods,
                                                                                          StringBuilder? cfg)
        {
            var ret_value = new List<(string LHS, string RHS, int Index)>();
            if (cfg is StringBuilder)
            {
                foreach (var p in prods)
                {                   
                    List<int>? applicableIndicies = getListOfApplicableIndices(p, cfg);
                    if (applicableIndicies is List<int>)
                    {
                        foreach (var i in applicableIndicies)
                        {
                            ret_value.Add((p.LHS, p.RHS, i));
                        }
                    }
                }
            }
            else
                throw new CfgIsNullException();

            return ret_value;
        }
    }
}
