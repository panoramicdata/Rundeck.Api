using AwesomeAssertions;
using Rundeck.Api.Exceptions;
using Rundeck.Api.Models;
using Rundeck.Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Rundeck.Api.Test.Documented
{
	[Collection("ProjectTests")]
	public class WebHooksTests : ProjectTestBase
	{
		public WebHooksTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async Task WebHooks_GetAll_Ok()
		{
			var webHooks = await GetWebHooksAsync();

			webHooks.Should().NotBeNull();
			webHooks.Should().BeEmpty();
		}

		[Fact]
		public async Task WebHooks_Create_Ok()
		{
			var webHookCreationResult = await CreateWebHookForNewJobAsync();

			webHookCreationResult.Should().NotBeNullOrEmpty();

			var webHooks = await GetWebHooksAsync();

			webHooks.Should().ContainSingle();

			await RundeckClient
				.WebHooks
				.DeleteAsync("Test", webHooks[0].Id, TestContext.Current.CancellationToken);
		}

		[Fact]
		public async Task WebHooks_Update_Ok()
		{
			await CreateWebHookForNewJobAsync();
			var webHooks = await GetWebHooksAsync();

			webHooks[0].Name.Should().Be("Test webhook");

			await RundeckClient
				.WebHooks
				.UpdateAsync("Test", webHooks[0].Id, new WebHook
				{
					// Rundeck 6 finds the webhook by uuid and rejects an update missing user, roles or eventPlugin
					Id = webHooks[0].Id,
					Uuid = webHooks[0].Uuid,
					Project = "Test",
					Name = "Updated webhook",
					User = webHooks[0].User,
					Roles = webHooks[0].Roles,
					EventPlugin = webHooks[0].EventPlugin,
					Enabled = webHooks[0].Enabled,
					Config = new WebHookConfig
					{
						JobId = webHooks[0].Config.JobId
					}
				}, TestContext.Current.CancellationToken);

			webHooks = await GetWebHooksAsync();

			webHooks[0].Name.Should().Be("Updated webhook");
		}

		[Fact]
		public async Task Webhooks_Delete_Ok()
		{
			await CreateWebHookForNewJobAsync();
			var webHooks = await GetWebHooksAsync();

			webHooks.Should().ContainSingle();

			await RundeckClient
				.WebHooks
				.DeleteAsync("Test", webHooks[0].Id, TestContext.Current.CancellationToken);

			webHooks = await GetWebHooksAsync();

			webHooks.Should().NotBeNull();
			webHooks.Should().BeEmpty();
		}

		[Fact]
		public async Task SendWebhookEvent_Passes()
		{
			await CreateWebHookForNewJobAsync();
			var webHooks = await GetWebHooksAsync();

			var webhookEventResult = await RundeckClient
				.WebHooks
				.SendEventAsync(webHooks[0].AuthToken, TestContext.Current.CancellationToken);

			webhookEventResult.Should().NotBeNullOrEmpty();
			webhookEventResult.Should().Contain("ok");
		}

		[Fact]
		public async Task SendWebhookEvent_InvalidAuthToken_NotAuthorized()
		{
			await CreateWebHookForNewJobAsync();

			Func<Task> act = async () => await RundeckClient
				.WebHooks
				.SendEventAsync("invalid_auth_token");

			await act.Should().ThrowAsync<NotAuthorizedException>();
		}

		private Task<List<WebHook>> GetWebHooksAsync()
			=> RundeckClient
				.WebHooks
				.GetAllAsync("Test", TestContext.Current.CancellationToken);

		// A webhook requires a job id, so import a job first
		private async Task<string> CreateWebHookForNewJobAsync()
		{
			await ImportJobAsync();
			var job = await GetSingleJobAsync();

			return await RundeckClient
				.WebHooks
				.CreateAsync("Test", new WebHookCreationDto
				{
					Config = new WebHookConfig
					{
						JobId = job.Id
					},
					Enabled = true,
					EventPlugin = "log-webhook-event",
					Name = "Test webhook",
					Project = "Test",
					Roles = "webhook,admin,user",
					User = "admin"
				});
		}
	}
}
