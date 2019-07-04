using System;
using System.Xml.Serialization;
using Infrastructure;
using Infrastructure.Logging;
using Newtonsoft.Json;

namespace Examples.Serializable
{
	[Run]
	public class Json : BaseSynchronousExample
	{
		public Json(ILogger logger)
		{
			Logger = logger;
		}

		public override void Run()
		{
			User adminUser = new Admin
			{
				Id = Guid.NewGuid(),
				Name = "Vladyslav",
				Surname = "Yefremov",
				Access = "Read | Write"
			};

			var serializedString = JsonConvert.SerializeObject(adminUser);

			// Writes: {"Access":"Read | Write", "Name":"Vladyslav", "Surname":"Yefremov"}
			Logger.Write(serializedString);
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