using FluentAssertions;
using Rundeck.Api.Models;
using Xunit;
using Xunit.Abstractions;

namespace Rundeck.Api.Test.Documented
{
	public class ProjectsTests : TestBase
	{
		public ProjectsTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async void Projects_CreateReadDelete_Ok()
		{
			// Ensure there are no existing projects
			var projects = await RundeckClient
				.Projects
				.GetAllAsync()
				.ConfigureAwait(false);

			projects.Should().NotBeNull();
			projects.Should().BeEmpty();

			try
			{
				// Create a project
				var project = await RundeckClient
						.Projects
						.CreateAsync(new Project
						{
							Description = "Test project",
							Name = "Test",
							Url = "example.com",
							Config = new Config()
						}
						)
						.ConfigureAwait(false);

				project.Should().NotBeNull();
				// Todo - test Project properties

				projects = await RundeckClient
					.Projects
					.GetAllAsync()
					.ConfigureAwait(false);

				projects.Should().NotBeNullOrEmpty();
				projects[0].Name.Should().Be(project.Name);
				projects[0].Url.Should().Be(project.Url);
				projects[0].Description.Should().Be(project.Description);

				// Get a Project by name
				var testProject = await RundeckClient
					.Projects
					.GetAsync("Test")
					.ConfigureAwait(false);

				testProject.Should().NotBeNull();
				testProject.Should().BeEquivalentTo(project);
			}
			finally
			{
				// Cleanup
				await RundeckClient
					  .Projects
					  .DeleteAsync("Test")
					  .ConfigureAwait(false);

				// Ensure there are no projects after cleanup
				projects = await RundeckClient
				.Projects
				.GetAllAsync()
				.ConfigureAwait(false);

				projects.Should().NotBeNull();
				projects.Should().BeEmpty();
			}
		}

		[Fact]
		public async void Projects_GetProjectConfig_Ok()
		{
			try
			{
				// Arrange - create a project
				var project = await RundeckClient
							.Projects
							.CreateAsync(new Project
							{
								Description = "Test project",
								Name = "Test",
								Url = "example.com",
								Config = new Config()
							}
							)
							.ConfigureAwait(false);

				project.Should().NotBeNull();

				var config = await RundeckClient
					.Projects
					.GetConfigAsync("Test")
					.ConfigureAwait(false);

				config.Should().NotBeNull();
				config.Should().BeEquivalentTo(project.Config);
			}
			finally
			{
				// Cleanup
				await RundeckClient
					  .Projects
					  .DeleteAsync("Test")
					  .ConfigureAwait(false);
			}
		}

		[Fact]
		public async void Projects_GetProjectResources_Ok()
		{
			// Arrange - create a project
			var project = await RundeckClient
						.Projects
						.CreateAsync(new Project
						{
							Description = "Test project",
							Name = "Test",
							Url = "example.com",
							Config = new Config()
						}
						)
						.ConfigureAwait(false);

			project.Should().NotBeNull();

			try
			{
				var resources = await RundeckClient
					.Projects
					.GetResourcesAsync("Test")
					.ConfigureAwait(false);

				resources.Should().NotBeNull();
				// Todo - check Resource properties
			}
			finally
			{
				// Cleanup
				await RundeckClient
					  .Projects
					  .DeleteAsync("Test")
					  .ConfigureAwait(false);
			}
		}

		[Fact]
		public async void Projects_GetProjectSources_Ok()
		{
			// Arrange - create a project
			var project = await RundeckClient
						.Projects
						.CreateAsync(new Project
						{
							Description = "Test project",
							Name = "Test",
							Url = "example.com",
							Config = new Config()
						}
						)
						.ConfigureAwait(false);

			project.Should().NotBeNull();

			try
			{
				var sources = await RundeckClient
					.Projects
					.GetSourcesAsync("Test")
					.ConfigureAwait(false);

				sources.Should().NotBeNull();
				// Todo - check Source properties
			}
			finally
			{
				// Cleanup
				await RundeckClient
					  .Projects
					  .DeleteAsync("Test")
					  .ConfigureAwait(false);
			}
		}
	}
}
