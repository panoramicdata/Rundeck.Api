using FluentAssertions;
using Rundeck.Api.Models;
using Rundeck.Api.Models.Dtos;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Rundeck.Api.Test.Documented
{
	[Collection("ProjectTests")]
	public class WebHooksTests : TestBase, IAsyncLifetime
	{
		public WebHooksTests(ITestOutputHelper output) : base(output)
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

		public async Task DisposeAsync()
		{
			// Remove the Project
			await RundeckClient
					  .Projects
					  .DeleteAsync("Test")
					  .ConfigureAwait(false);
		}

		[Fact]
		public async void WebHooks_GetAll_Ok()
		{
			var webHooks = await RundeckClient
				.WebHooks
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			webHooks.Should().NotBeNull();
			webHooks.Should().BeEmpty();
		}

		[Fact]
		public async void WebHooks_Create_Ok()
		{
			// Arrange
			// Create a job as WebHook requires a job Id
			await ImportJobAsync().ConfigureAwait(false);
			var jobs = await RundeckClient
				.Jobs
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			jobs.Should().NotBeNull();
			jobs.Should().ContainSingle();

			var webHookCreationResult = await RundeckClient
				.WebHooks
				.CreateAsync("Test", new WebHookCreationDto
				{
					Config = new WebHookConfig
					{
						JobId = jobs[0].Id
					},
					Enabled = true,
					EventPlugin = "webhook-run-job",
					Name = "Test webhook",
					Project = "Test",
					Roles = "webhook,admin,user",
					User = "admin"

				}
				).ConfigureAwait(false);

			webHookCreationResult.Should().NotBeNullOrEmpty();

			var webHooks = await RundeckClient
				.WebHooks
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			webHooks.Should().ContainSingle();

			// Cleanup
			await RundeckClient
				.WebHooks
				.DeleteAsync("Test", webHooks[0].Id)
				.ConfigureAwait(false);
		}
	}
}
