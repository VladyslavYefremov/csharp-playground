using System.Collections.ObjectModel;
using System.Linq;
using Composition.Resolving;

namespace Client
{
	public class ExamplesDataContext
	{
		public ExamplesDataContext()
		{
			var resolver = new Resolver();
				
			var asynchronousExamples = resolver.GetAllAsyncExamples().ToList();
			var synchronousExamples = resolver.GetAllSyncExamples().ToList();

			Tabs = new ObservableCollection<TabItem>();

			foreach (var example in asynchronousExamples)
			{
				Tabs.Add(new TabItem { Header = example.GetType().Name });
			}
		}
		public ObservableCollection<TabItem> Tabs { get; set; }

		public class TabItem
		{
			public string Header { get; set; }
		}
	}
}