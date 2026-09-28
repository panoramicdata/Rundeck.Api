using AwesomeAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Neovolve.Logging.Xunit;
using Rundeck.Api.Models;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Rundeck.Api.Test
{
	/// <summary>
	/// Base class for tests that call a live Rundeck. Every test deriving from it is an integration test:
	/// CI has no Rundeck credentials, so it excludes this category (Category!=Integration).
	/// </summary>
	[Trait("Category", "Integration")]
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

			// Rundeck:Uri, Rundeck:Token and Rundeck:Username come from user secrets (dotnet user-secrets, UserSecretsId
			// rundeck-api-tests) or from environment variables (Rundeck__Uri, Rundeck__Token, Rundeck__Username).
			TestConfig = new ConfigurationBuilder()
				.AddUserSecrets<TestBase>()
				.AddEnvironmentVariables()
				.Build()
				.GetSection("Rundeck")
				.Get<TestConfig>() ?? new TestConfig();
			if (TestConfig.Uri is null || string.IsNullOrWhiteSpace(TestConfig.Token))
			{
				throw new InvalidOperationException("Rundeck:Uri and Rundeck:Token are not configured in user secrets or the environment.");
			}

			RundeckClient = new RundeckClient(
				new RundeckClientOptions
				{
					Uri = TestConfig.Uri,
					ApiToken = TestConfig.Token,
					Logger = Logger
				});
		}


		public async Task<JobImportResult> ImportJobAsync()
			=> await ImportJobAsync(JobUuidOption.Preserve);

		public async Task<JobImportResult> ImportJobAsync(JobUuidOption uuidOption)
		{
			const string jobContents = @"
- defaultTab: nodes
  description: test job
  executionEnabled: false
  id: a4fc12f7-a993-4cee-af01-4aececa0401d
  loglevel: INFO
  name: Test job
  nodeFilterEditable: false
  options:
  - description: option description
    name: myfile
    type: file
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
				.ImportAsync("Test", jobContents, JobFileFormat.YAML, uuidOption)
				;

			jobImportResults.Succeeded.Should().ContainSingle();
			jobImportResults.Failed.Should().BeEmpty();
			jobImportResults.Skipped.Should().BeEmpty();

			return jobImportResults.Succeeded.Single();
		}
	}
}