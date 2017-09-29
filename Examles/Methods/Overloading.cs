using Infrastructure;
using Infrastructure.Logging;

namespace Examles.Methods
{
	[Run]
	public class Overloading : BaseSynchronousExample
	{
		public Overloading(ILogger logger)
		{
			Logger = logger;
		}

		public override void Run()
		{
			Base baseTypeDerivedObject = new Derived();

			var derived = new Derived();

			ShowName(baseTypeDerivedObject);
			ShowName(derived);
		}

		private void ShowName(Base obj)
		{
			Logger.Write("Called method with 'base class' argument: ", MessageColor.Yellow);
			Logger.Write(obj.GetName(), MessageColor.Yellow);
		}

		private void ShowName(Derived obj)
		{
			Logger.Write("Called method with 'derived class' argument: ", MessageColor.Blue);
			Logger.Write(obj.GetName(), MessageColor.Blue);
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