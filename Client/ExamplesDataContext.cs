﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Composition.Resolving;
using Infrastructure;
using Infrastructure.Logging;

namespace Client
{
	public class ExamplesDataContext
	{
		public ExamplesDataContext()	
		{
			var resolver = new Resolver();
				
			// get examples
			var asynchronousExamples = resolver.GetAllAsyncExamples().ToList();
			var synchronousExamples = resolver.GetAllSyncExamples().ToList();

			// union examles and cast to base type
			var examples = asynchronousExamples.Cast<BaseExample>()
				.Concat(synchronousExamples);

			// generate tabs
			CreateTabsFromExamples(examples);

			// execute
			StartAsynchronousExamples(asynchronousExamples);
			StartSyncronousExamples(synchronousExamples);
		}

		public ObservableCollection<TabItem> Tabs { get; set; }

		private void CreateTabsFromExamples(IEnumerable<BaseExample> examples)
		{
			Tabs = new ObservableCollection<TabItem>();

			foreach (var example in examples)
			{
				var tab = new TabItem { Header = example.GetType().Name };

				// subscribe to output from example
				example.Logger.OnNewMessage += (msg) => tab.Messages.Add(msg);

				Tabs.Add(tab);
			}
		}

		private void StartAsynchronousExamples(IEnumerable<BaseAsyncExample> examples)
		{
			foreach (var example in examples)
			{
				example.RunAsync(); // fire and forget
			}
		}

		private void StartSyncronousExamples(IEnumerable<BaseSynchronousExample> examples)
		{
			foreach (var example in examples)
			{
				Task.Run(() => example.Run()); // fire and forget
			}
		}

		public class TabItem
		{
			public TabItem()
			{
				Messages = new ObservableCollection<Message>();
			}

			public string Header { get; set; }
			public ObservableCollection<Message> Messages { get; set; }
		}
	}
}