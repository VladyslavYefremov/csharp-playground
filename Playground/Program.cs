using System;
using Playground.Asynchronous;

namespace Playground
{
	internal class Program
	{
		private static void Main()
		{
			new Start().RunAsync().Wait();

			Console.ReadKey();
		}
	}
}
