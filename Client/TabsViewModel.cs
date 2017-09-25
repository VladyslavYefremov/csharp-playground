using System.Collections.ObjectModel;

namespace Client
{
	public class TabsViewModel
	{
		public ObservableCollection<TabItem> Tabs { get; set; }

		public TabsViewModel()	
		{
			Tabs = new ObservableCollection<TabItem>
			{
				new TabItem
				{
					Header = "Test 1"
				},
				new TabItem
				{
					Header = "Test 3"
				}
			};
		}

		public class TabItem
		{
			public string Header { get; set; }
		}
	}
}