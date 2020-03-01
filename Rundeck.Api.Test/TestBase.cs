using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using Xunit.Abstractions;

namespace Rundeck.Api.Test
{
	public abstract class TestBase
	{
		/// <summary>
		/// A logger
		/// </summary>
		protected ILogger Logger { get; }
		public TestConfig TestConfig { get; }

		/// <summary>
		/// The client to use in tests
		/// </summary>
		protected RundeckClient RundeckClient { get; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="output"></param>
		protected TestBase(ITestOutputHelper output)
		{
			Logger = output.BuildLogger();
			Logger.LogInformation("Test started");

			TestConfig = JsonConvert.DeserializeObject<TestConfig>(File.ReadAllText("../../../appsettings.json"));

			RundeckClient = new RundeckClient(
				new RundeckClientOptions
				{
					Uri = new Uri(TestConfig.Uri),
					ApiToken = TestConfig.Token,
					Logger = Logger
				});
		}
	}
}