using AwesomeAssertions;
using Rundeck.Api.Models;
using System.Threading.Tasks;
using Xunit;

namespace Rundeck.Api.Test.Documented
{
	[Collection("ProjectTests")]
	public class ProjectsTests : TestBase, IAsyncLifetime
	{
		private Project? _project;

		public ProjectsTests(ITestOutputHelper output) : base(output)
		{
		}

		public async ValueTask InitializeAsync()
		{
			_project = await RundeckClient
				.Projects
				.CreateAsync(new Project
				{
					Description = "Test project",
					Name = "Test",
					Url = "example.com",
					Config = new Config()
				}
				);

			_project.Should().NotBeNull();
		}

		public async ValueTask DisposeAsync() =>
			// Remove the Project
			await RundeckClient
					  .Projects
					  .DeleteAsync("Test")
					  ;

		[Fact]
		public async Task Projects_CreateReadDelete_Ok()
		{
			try
			{
				// Create a project
				var project = await RundeckClient
						.Projects
						.CreateAsync(new Project
						{
							Description = "Create a project for testing",
							Name = "Project",
							Url = "example.com",
							Config = new Config()
						}, TestContext.Current.CancellationToken);

				project.Should().NotBeNull();
				// Todo - test Project properties

				var projects = await RundeckClient
					.Projects
					.GetAllAsync(TestContext.Current.CancellationToken);

				projects.Should().NotBeNullOrEmpty();
				projects.Should().HaveCount(2);

				// Get a Project by name
				var testProject = await RundeckClient
					.Projects
					.GetAsync("Project", TestContext.Current.CancellationToken);

				testProject.Should().NotBeNull();
				testProject.Should().BeEquivalentTo(project);
			}
			finally
			{
				// Cleanup
				await RundeckClient
					  .Projects
					  .DeleteAsync("Project", TestContext.Current.CancellationToken);
			}
		}

		[Fact]
		public async Task Projects_GetProjectConfig_Ok()
		{
			var config = await RundeckClient
				.Projects
				.GetConfigAsync(_project!.Name, TestContext.Current.CancellationToken);

			config.Should().NotBeNull();
			config.Should().BeEquivalentTo(_project!.Config);
		}

		[Fact]
		public async Task Projects_GetProjectResources_Ok()
		{
			var resources = await RundeckClient
				.Projects
				.GetResourcesAsync(_project!.Name, TestContext.Current.CancellationToken);

			resources.Should().NotBeNull();
			// Todo - check Resource properties
		}

		[Fact]
		public async Task Projects_GetProjectSources_Ok()
		{
			var sources = await RundeckClient
				.Projects
				.GetSourcesAsync(_project!.Name, TestContext.Current.CancellationToken);

			sources.Should().NotBeNull();
			// Todo - check Source properties
		}

		[Fact]
		public async Task Projects_RunAdhocCommand_Passes()
		{
			// Arrange
			var command = new AdhocCommand
			{
				Project = _project!.Name,
				Exec = "echo hello world",
			};

			// Act
			var response = await RundeckClient
				.Projects
				.RunCommandAsync(_project!.Name, command, TestContext.Current.CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Message.Should().Contain("Immediate execution scheduled");
		}
	}
}
