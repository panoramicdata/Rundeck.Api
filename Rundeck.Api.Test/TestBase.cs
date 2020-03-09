using FluentAssertions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rundeck.Api.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
			Logger.LogInformation(Resources.TestStarted);

			TestConfig = JsonConvert.DeserializeObject<TestConfig>(File.ReadAllText("../../../appsettings.json"));

			RundeckClient = new RundeckClient(
				new RundeckClientOptions
				{
					Uri = TestConfig.Uri,
					ApiToken = TestConfig.Token,
					Logger = Logger
				});
		}

		public async Task<JobImportResult> ImportJobAsync()
		{
			const string jobContents = @"
- defaultTab: nodes
  description: test job
  executionEnabled: false
  id: a4fc12f7-a993-4cee-af01-4aececa0401d
  loglevel: INFO
  name: Test  job
  nodeFilterEditable: false
  schedule:
    month: '*'
    time:
      hour: '23'
      minute: '18'
      seconds: '0'
    weekday:
      day: '*'
    year: '*'
  scheduleEnabled: false
  sequence:
    commands:
    - description: test step
      exec: pwd
    keepgoing: false
    strategy: node-first
  uuid: a4fc12f7-a993-4cee-af01-4aececa0401d";

			var jobImportResults = await RundeckClient
				.Jobs
				.ImportAsync("Test", jobContents, JobFileFormat.YAML)
				.ConfigureAwait(false);

			jobImportResults.Succeeded.Should().ContainSingle();
			jobImportResults.Failed.Should().BeEmpty();
			jobImportResults.Skipped.Should().BeEmpty();

			return jobImportResults.Succeeded.Single();
		}
	}
}