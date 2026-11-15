using AwesomeAssertions;
using Rundeck.Api.Models;
using Rundeck.Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Rundeck.Api.Test.Documented
{
	public class AuthenticationTokensTests : TestBase
	{
		public AuthenticationTokensTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async Task AuthenticationTokens_GetAllAsync_Passes()
		{
			await AssertNoTokensAsync();

			await WithNewTokenAsync(async newToken =>
			{
				var authenticationTokens = await RundeckClient
					.AuthenticationTokens
					.GetAllAsync(TestContext.Current.CancellationToken);

				authenticationTokens.Should().NotBeNullOrEmpty();
				authenticationTokens.Should().ContainSingle();
				authenticationTokens[0].Id.Should().Be(newToken.Id);
				authenticationTokens[0].User.Should().Be(TestConfig.Username);
				authenticationTokens[0].Creator.Should().Be(TestConfig.Username);
				authenticationTokens[0].Expired.Should().BeFalse();
				authenticationTokens[0].Expiration.Should().Be(newToken.Expiration);
				authenticationTokens[0].Roles.Should().NotBeNullOrEmpty();
				// Token lists don't return the actual token, use Get with an Id
				authenticationTokens[0].Token.Should().BeEmpty();
			});

			await AssertNoTokensAsync();
		}

		[Fact]
		public async Task AuthenticationTokens_GetAllByUserAsync_Passes()
		{
			var authenticationTokens = await GetAllByUserAsync();
			authenticationTokens.Should().NotBeNull();
			authenticationTokens.Should().BeEmpty();

			await WithNewTokenAsync(async newToken =>
			{
				authenticationTokens = await GetAllByUserAsync();

				authenticationTokens.Should().NotBeNullOrEmpty();
				authenticationTokens[0].Id.Should().Be(newToken.Id);
				authenticationTokens[0].Token.Should().BeEmpty();
			});
		}

		[Fact]
		public Task CreateDelete_Succeeds()
			=> WithNewTokenAsync(newToken =>
			{
				newToken.Should().NotBeNull();
				newToken.Id.Should().NotBeNull();
				newToken.User.Should().Be(TestConfig.Username);
				newToken.Creator.Should().Be(TestConfig.Username);
				newToken.Expired.Should().BeFalse();
				newToken.Expiration.Should().BeAfter(DateTimeOffset.UtcNow);
				newToken.Roles.Should().NotBeNullOrEmpty();
				newToken.Token.Should().NotBeNullOrEmpty();
				return Task.CompletedTask;
			});

		[Fact]
		public Task Get_ExistingToken_Succeeds()
			=> WithNewTokenAsync(async newToken =>
			{
				var existingToken = await RundeckClient
					.AuthenticationTokens
					.GetAsync(newToken.Id, TestContext.Current.CancellationToken);

				existingToken.Should().NotBeNull();
				existingToken.Id.Should().Be(newToken.Id);
				existingToken.User.Should().Be(TestConfig.Username);
				existingToken.Creator.Should().Be(TestConfig.Username);
				existingToken.Expired.Should().BeFalse();
				existingToken.Expiration.Should().Be(newToken.Expiration);
				existingToken.Roles.Should().NotBeEmpty();
				existingToken.Roles.Should().BeEquivalentTo(newToken.Roles);
				// Rundeck 3.3+ returns the token value only when it is created
				newToken.Token.Should().NotBeNullOrEmpty();
				existingToken.Token.Should().BeEmpty();
			});

		private Task<List<AuthenticationToken>> GetAllByUserAsync()
			=> RundeckClient
				.AuthenticationTokens
				.GetAllByUserAsync(TestConfig.Username, TestContext.Current.CancellationToken);

		private async Task AssertNoTokensAsync()
		{
			var authenticationTokens = await RundeckClient
				.AuthenticationTokens
				.GetAllAsync(TestContext.Current.CancellationToken);

			authenticationTokens.Should().NotBeNull();
			authenticationTokens.Should().BeEmpty();
		}

		private async Task WithNewTokenAsync(Func<AuthenticationToken, Task> check)
		{
			var newToken = await RundeckClient
				.AuthenticationTokens
				.CreateAsync(new AuthenticationTokenCreationDto
				{
					User = TestConfig.Username,
					Roles = ["*"]
				}, TestContext.Current.CancellationToken);

			try
			{
				await check(newToken);
			}
			finally
			{
				await RundeckClient
					.AuthenticationTokens
					.DeleteAsync(newToken.Id, TestContext.Current.CancellationToken);
			}
		}
	}
}
