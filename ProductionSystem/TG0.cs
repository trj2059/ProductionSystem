using System;
using System.Collections.Generic;
using System.Text;

#nullable enable

namespace ProductionSystem
{
	/// <summary>
	/// Implemenation of a Type 0 Chomskey Grammar and
	/// various operations that can be applied on the grammar.
	/// </summary>
	public class T0G
	{
		private List<List<(string LHS, string RHS)>> prods { get; set; }

		private StringBuilder? config { get; set; }

		/// <summary>
		/// Default constructor with some test productions and a default
		/// start symbol loaded in the configuation
		/// </summary>
		public T0G()
		{
			prods = new List<List<(string LHS, string RHS)>>();
			prods.Add(new List<(string LHS, string RHS)>() { ("S", "Sa") });
			prods.Add(new List<(string LHS, string RHS)>() { ("S", "aAb") });
			prods.Add(new List<(string LHS, string RHS)>() { ("A", "aA") });
			prods.Add(new List<(string LHS, string RHS)>() { ("A", "a") } );
			config = new StringBuilder("S");
		}

		// TODO: Finish documetation this constructor.
		/// <summary>
		/// Constructor that loads up 
		/// </summary>
		/// <param name="prods"></param>
		/// <param name="config"></param>
		public T0G(List<List<(string LHS, string RHS)>> prods, StringBuilder config)
		{
			this.prods = prods;
			this.config = config;
		}

		/// <summary>
		/// Determines if a given production can be applied to a configuration.
		/// </summary>
		/// <param name="prod">A given production</param>
		/// <param name="cfg">The current grammar configuraton</param>
		/// <param name="index">The index of the symbol on the grammar to be substituted</param>
		/// <returns>Returns true if the production can be applied to the configuation, false otherwise.</returns>
		private bool? canApplyProd((string LHS, string RHS) prod, StringBuilder cfg, int index)
		{
			try
			{
				if (index < 0)
					throw new Exception("Index less than 0");
				if (index > cfg.Length - 1)
					throw new Exception("Index greater than cfg length");

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
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return null;
			}
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
		private StringBuilder? applyProd((string LHS, string RHS) prod, StringBuilder? cfg, int index)
		{
			try
			{
				if (cfg is StringBuilder)
				{
					cfg.Remove(index, prod.LHS.Length);
					cfg.Insert(index, prod.RHS);
					return cfg;
				}
				else
				{
					throw new Exception("cfg is null");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return null;
			}
		}


		/// <summary>
		/// Finds the list of all indicies you can apply a given production to a cfg
		/// Attemps to apply a production's LHS to all possible indices of a cfg
		/// up to the cfg.Length - lhs.length.
		/// </summary>
		/// <param name="prod">The  production to be used in the test</param>
		/// <param name="cfg">The configuration that is to be tested against</param>
		/// <returns>A list of all applicable indicies the lhs can be applied to the cfg</returns>
		private List<int>? getListOfApplicableIndices((string LHS, string RHS) prod, StringBuilder? cfg)
		{
			// TODO : Finish getListOfApplicableIndices
			try
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
							throw new Exception("Can apply prod returned null");
					}
				}
				else
					throw new Exception("cfg is null");

				return ret_value;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return null;
			}
		}

		/// <summary>
		/// Determins what productions can be applied to a given configuation
		/// </summary>
		/// <param name="prods">A list of productions</param>
		/// <param name="cfg">A grammar configuration</param>
		/// <returns>All applicable productions.  Returns null if there is an exception.</returns>
		private List<(string LHS, string RHS, int Index)>? allApplicableProductions(List<(string LHS, string RHS)> prods, StringBuilder? cfg)
		{
			try
			{
				var ret_value = new List<(string LHS, string RHS, int Index)>();

				if (cfg is StringBuilder)
				{
					foreach (var p in prods)
					{
						// UNDONE: check if production is applicable to cfg
						List<int>? applicableIndicies = getListOfApplicableIndices(p, cfg);
						if (applicableIndicies is List<int>)
						{
							foreach (var i in applicableIndicies)
							{
								ret_value.Add( (p.LHS, p.RHS, i) );
							}
						}
					}
				}
				else
					throw new Exception("cfg is null");

				return ret_value;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return null;
			}
		}

		public StringBuilder? Test1()
		{
			try
			{
				if (config is StringBuilder)
				{
					Console.WriteLine(config.ToString());

					if (canApplyProd(prods[0][0], config, 0) is bool)
					{
						config = applyProd(prods[0][0], config, 0);
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
