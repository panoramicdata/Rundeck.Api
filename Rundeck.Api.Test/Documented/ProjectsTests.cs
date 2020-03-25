using FluentAssertions;
using Rundeck.Api.Models;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Rundeck.Api.Test.Documented
{
	[Collection("ProjectTests")]
	public class ProjectsTests : TestBase, IAsyncLifetime
	{
		private Project? _project;

		public ProjectsTests(ITestOutputHelper output) : base(output)
		{
		}

		public async Task InitializeAsync()
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
				).ConfigureAwait(false);

			_project.Should().NotBeNull();
		}

		public async Task DisposeAsync() =>
			// Remove the Project
			await RundeckClient
					  .Projects
					  .DeleteAsync("Test")
					  .ConfigureAwait(false);

		[Fact]
		public async void Projects_CreateReadDelete_Ok()
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
						}
						)
						.ConfigureAwait(false);

				project.Should().NotBeNull();
				// Todo - test Project properties

				var projects = await RundeckClient
					.Projects
					.GetAllAsync()
					.ConfigureAwait(false);

				projects.Should().NotBeNullOrEmpty();
				projects.Should().HaveCount(2);

				// Get a Project by name
				var testProject = await RundeckClient
					.Projects
					.GetAsync("Project")
					.ConfigureAwait(false);

				testProject.Should().NotBeNull();
				testProject.Should().BeEquivalentTo(project);
			}
			finally
			{
				// Cleanup
				await RundeckClient
					  .Projects
					  .DeleteAsync("Project")
					  .ConfigureAwait(false);
			}
		}

		[Fact]
		public async void Projects_GetProjectConfig_Ok()
		{
			var config = await RundeckClient
				.Projects
				.GetConfigAsync(_project.Name)
				.ConfigureAwait(false);

			config.Should().NotBeNull();
			config.Should().BeEquivalentTo(_project.Config);
		}

		[Fact]
		public async void Projects_GetProjectResources_Ok()
		{
			var resources = await RundeckClient
				.Projects
				.GetResourcesAsync(_project.Name)
				.ConfigureAwait(false);

			resources.Should().NotBeNull();
			// Todo - check Resource properties
		}

		[Fact]
		public async void Projects_GetProjectSources_Ok()
		{
			var sources = await RundeckClient
				.Projects
				.GetSourcesAsync(_project.Name)
				.ConfigureAwait(false);

			sources.Should().NotBeNull();
			// Todo - check Source properties
		}

		[Fact]
		public async void Projects_RunAdhocCommand_Passes()
		{
			// Arrange
			var command = new AdhocCommand
			{
				Project = _project.Name,
				Exec = "echo hello world",
			};

			// Act
			var response = await RundeckClient
				.Projects
				.RunCommandAsync(_project.Name, command)
				.ConfigureAwait(false);

			// Assert
			response.Should().NotBeNull();
			response.Message.Should().Contain("Immediate execution scheduled");
		}
	}
}
