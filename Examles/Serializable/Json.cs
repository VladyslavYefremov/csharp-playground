using System;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Examles.Serializable
{
	public class Json
	{
		public void Run()
		{
			User adminUser = new Admin();

			var serializer = JsonConvert.SerializeObject(adminUser);

			Console.WriteLine(serializer);
		}

		private class User
		{
			[JsonIgnore, XmlIgnore]
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string Surname { get; set; }
		}

		private class Admin : User
		{
			public string Access { get; set; }
		}
	}
}