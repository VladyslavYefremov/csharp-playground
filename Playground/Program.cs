using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Infrastructure;

namespace Playground
{
	internal class Program
	{
		private static void Main()
		{
			var typesToRun = GetTypesToRun().ToList();

			var synchronousTypes = typesToRun.Where(type => type.IsSubclassOf(typeof(BaseSynchronousExample)));
			var asynchronousTypes = typesToRun.Where(type => type.IsSubclassOf(typeof(BaseAsyncExample)));

			SelectAndRunSynchronousTypes(synchronousTypes);
			SelectAndRunAsynchronousTypes(asynchronousTypes);

			Console.WriteLine("Press any key to exit...");
			Console.ReadKey();
		}

		private static IEnumerable<Type> GetTypesToRun()
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();

			return assemblies
				.SelectMany(assembly => assembly.GetTypes())
				.Where(type => type
					.GetCustomAttributes()
					.Any(attribute => attribute.GetType() == typeof(RunAttribute))
				);
		}

		private static object CreateInstanceOrGetNull(Type typeOfInstance)
		{
			try
			{
				return Activator.CreateInstance(typeOfInstance);
			}
			catch
			{
				return null;
			}
		} 

		private static void SelectAndRunSynchronousTypes(IEnumerable<Type> overallTypes)
		{
			var typesToRun = overallTypes
				.Select(CreateInstanceOrGetNull)
				.Select(instance => instance as BaseSynchronousExample)
				.Where(instance => instance != null);

			foreach (var type in typesToRun)
			{
				ShowNameAndDescription(type.GetType().FullName);
				type.Run();
			}	
		}
		private static void SelectAndRunAsynchronousTypes(IEnumerable<Type> overallTypes)
		{
			var typesToRun = overallTypes
				.Select(CreateInstanceOrGetNull)
				.Select(instance => instance as BaseAsyncExample)
				.Where(instance => instance != null);

			foreach (var type in typesToRun)
			{
				ShowNameAndDescription(type.GetType().FullName);
				type.RunAsync().Wait();
			}
		}

		private static void ShowNameAndDescription(string name, string description = "")
		{
			var lineString = new string('=', 10);

			Console.WriteLine();
			Console.WriteLine(lineString);
			Console.WriteLine($"Name: {name}");
			Console.WriteLine($"Description: {description}");
			Console.WriteLine(lineString);
			Console.WriteLine();
		}
	}
}
