using FluentAssertions;
using Rundeck.Api.Models;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Rundeck.Api.Test.Documented
{
	[Collection("ProjectTests")]
	public class ExecutionsTests : TestBase, IAsyncLifetime
	{
		public ExecutionsTests(ITestOutputHelper output) : base(output)
		{
		}

		public async Task InitializeAsync()
		{
			var project = await RundeckClient
				.Projects
				.CreateAsync(new Project
				{
					Description = "Test project",
					Name = "Test",
					Url = "example.com",
					Config = new Config()
				}
				).ConfigureAwait(false);

			project.Should().NotBeNull();
		}

		public async Task DisposeAsync() =>
			// Remove the Project
			await RundeckClient
					  .Projects
					  .DeleteAsync("Test")
					  .ConfigureAwait(false);

		[Fact]
		public async void Executions_GetAllForJob_Passes()
		{
			// Arrange
			var jobImportResult = await ImportJobAsync().ConfigureAwait(false);

			// Act
			var executions = await RundeckClient
				.Jobs
				.GetExecutionsAsync(jobImportResult.Id)
				.ConfigureAwait(false);

			// Assert
			executions.Should().NotBeNull();
			executions.Executions.Should().NotBeNull();
			executions.Executions.Should().BeEmpty();
		}
	}
}
