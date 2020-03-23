using FluentAssertions;
using Rundeck.Api.Exceptions;
using Rundeck.Api.Models;
using Rundeck.Api.Models.Dtos;
using System;
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

		// Executed before each test
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

		// Executed after each test
		public Task DisposeAsync() =>
			// Remove the Project, this also removes entities belonging to the Project
			RundeckClient
					  .Projects
					  .DeleteAsync("Test");

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
			var webHookCreationResult = await CreateWebHookAsync(jobs[0].Id).ConfigureAwait(false);

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

		[Fact]
		public async void WebHooks_Update_Ok()
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

			await CreateWebHookAsync(jobs[0].Id).ConfigureAwait(false);

			var webHooks = await RundeckClient
				.WebHooks
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			webHooks[0].Name.Should().Be("Test webhook");

			await RundeckClient
				.WebHooks
				.UpdateAsync("Test", webHooks[0].Id, new WebHook
				{
					Id = webHooks[0].Id,
					Project = "Test",
					Name = "Updated webhook",
					Config = new WebHookConfig
					{
						JobId = webHooks[0].Config.JobId
					}
				}).ConfigureAwait(false);

			webHooks = await RundeckClient
				.WebHooks
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			webHooks[0].Name.Should().Be("Updated webhook");
		}

		[Fact]
		public async void Webhooks_Delete_Ok()
		{
			// Create a job as WebHook requires a job Id
			await ImportJobAsync().ConfigureAwait(false);
			var jobs = await RundeckClient
				.Jobs
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			jobs.Should().NotBeNull();
			jobs.Should().ContainSingle();

			await CreateWebHookAsync(jobs[0].Id).ConfigureAwait(false);

			var webHooks = await RundeckClient
				.WebHooks
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			webHooks.Should().ContainSingle();

			await RundeckClient
				.WebHooks
				.DeleteAsync("Test", webHooks[0].Id)
				.ConfigureAwait(false);

			webHooks = await RundeckClient
				.WebHooks
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			webHooks.Should().NotBeNull();
			webHooks.Should().BeEmpty();
		}

		[Fact]
		public async void SendWebhookEvent_Passes()
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

			await CreateWebHookAsync(jobs[0].Id).ConfigureAwait(false);

			// Get the webhook we just created
			var webHooks = await RundeckClient
				.WebHooks
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			// Act - send Webhook event using the above created Webhook's auth_token
			var webhookEventResult = await RundeckClient
				.WebHooks
				.SendEventAsync(webHooks[0].AuthToken)
				.ConfigureAwait(false);

			webhookEventResult.Should().NotBeNullOrEmpty();
			webhookEventResult.Should().Contain("ok");
		}

		[Fact]
		public async void SendWebhookEvent_InvalidAuthToken_NotAuthorized()
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

			await CreateWebHookAsync(jobs[0].Id).ConfigureAwait(false);

			// Get the webhook we just created
			var webHooks = await RundeckClient
				.WebHooks
				.GetAllAsync("Test")
				.ConfigureAwait(false);

			// Act - send Webhook event using the above created Webhook's auth_token
			Func<Task> act = async () => await RundeckClient
				.WebHooks
				.SendEventAsync("invalid_auth_token")
				.ConfigureAwait(false);

			act.Should().Throw<NotAuthorizedException>();
		}

		private Task<string> CreateWebHookAsync(string jobId)
			=> RundeckClient
				.WebHooks
				.CreateAsync("Test", new WebHookCreationDto
				{
					Config = new WebHookConfig
					{
						JobId = jobId
					},
					Enabled = true,
					EventPlugin = "log-webhook-event",
					Name = "Test webhook",
					Project = "Test",
					Roles = "webhook,admin,user",
					User = "admin"
				}
				);
	}
}
