﻿using System;
using System.Linq;

namespace Playground.Linq
{
	public class MultipleExecution
	{
		private readonly Random _random;

		public MultipleExecution()
		{
			_random = new Random();
		}

		public void Run()
		{
			var numbers = Enumerable.Range(1, 3);

			var identifiers = numbers.Select(GetStringById);

			Console.WriteLine(Environment.NewLine + "Creating 1st list");
			var idListFirst = identifiers.ToList();

			Console.WriteLine(Environment.NewLine + "Creating 2nd list");
			var idListSecond = identifiers.ToList();

			Console.WriteLine(Environment.NewLine + "Enumerable identifiers:");
			foreach (var idString in identifiers)
			{
				Console.WriteLine(idString);
			}

			Console.WriteLine(Environment.NewLine + "First list:");
			foreach (var idString in idListFirst)
			{
				Console.WriteLine(idString);
			}

			Console.WriteLine(Environment.NewLine + "Second list:");
			foreach (var idString in idListSecond)
			{
				Console.WriteLine(idString);
			}

			/**
			 * As a result every sequese contains different values 
			 * generated by GetStringById method because
			 * of multyple enumeration of IEnumerable
			 */

			/** Output:
			 * 
			 *  Creating 1st list
			 *	GetStringById was called for id: 1
			 *	GetStringById was called for id: 2
			 *	GetStringById was called for id: 3
			 *
			 *	Creating 2nd list
			 *	GetStringById was called for id: 1
			 *	GetStringById was called for id: 2
			 *	GetStringById was called for id: 3
			 *
			 *	Enumerable identifiers:
			 *	GetStringById was called for id: 1
			 *	 1. 2900
			 *	GetStringById was called for id: 2
			 *	 2. 4505
			 *	GetStringById was called for id: 3
			 *	 3. 2521
			 *
			 *	First list:
			 *	 1. 3349
			 *	 2. 3528
			 *	 3. 8270
			 *
			 *	Second list:
			 *	 1. 3553
			 *	 2. 3422
			 *	 3. 8449
			 */
		}

		private string GetStringById(int id)
		{
			Console.WriteLine($"GetStringById was called for id: {id}");

			return $"{id,2}. {_random.Next(1000, 9999)}";
		}
	}
}