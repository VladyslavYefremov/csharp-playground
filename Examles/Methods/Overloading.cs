using System;

namespace Examles.Methods
{
	public class Overloading
	{
		public void Run()
		{
			Base baseTypeDerivedObject = new Derived();

			var derived = new Derived();

			ShowName(baseTypeDerivedObject);
			ShowName(derived);
		}

		private static void ShowName(Base obj)
		{
			Console.WriteLine("Called method with 'base class' argument: ");
			Console.WriteLine(obj.GetName());
		}

		private static void ShowName(Derived obj)
		{
			Console.WriteLine("Called method with 'derived class' argument: ");
			Console.WriteLine(obj.GetName());
		}

		private class Base
		{
			public virtual string GetName()
			{
				return "I'm base class.";
			}
		}
		private class Derived : Base
		{
			public override string GetName()
			{
				return "I'm derived class.";
			}
		}
	}
}