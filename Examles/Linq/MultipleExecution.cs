﻿using System;
using System.Linq;
using Infrastructure;
using Infrastructure.Logging;

namespace Examles.Linq
{
	[Run]
	public class MultipleExecution : BaseSynchronousExample
	{
		private readonly Random _random;

		public MultipleExecution(ILogger logger)
		{
			Logger = logger;
			_random = new Random();
		}

		public override void Run()
		{
			var numbers = Enumerable.Range(1, 3);

			var identifiers = numbers.Select(GetStringById);

			Logger.Write("Creating 1st list");
			var idListFirst = identifiers.ToList();

			Logger.Write("Creating 2nd list");
			var idListSecond = identifiers.ToList();

			Logger.Write("Enumerable identifiers:");
			foreach (var idString in identifiers)
			{
				Logger.Write(idString, MessageColor.Yellow);
			}

			Logger.Write("First list:");
			foreach (var idString in idListFirst)
			{
				Logger.Write(idString, MessageColor.Green);
			}

			Logger.Write("Second list:");
			foreach (var idString in idListSecond)
			{
				Logger.Write(idString, MessageColor.Blue);
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
			Logger.Write($"GetStringById was called for id: {id}");

			return $"{id,2}. {_random.Next(1000, 9999)}";
		}
	}
}