using FluentAssertions;
using Rundeck.Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

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
			var authenticationTokens = await RundeckClient
				.AuthenticationTokens
				.GetAllAsync()
				;

			authenticationTokens.Should().NotBeNull();
			authenticationTokens.Should().BeEmpty();

			// Create a token
			var newToken = await RundeckClient
				.AuthenticationTokens
				.CreateAsync(new AuthenticationTokenCreationDto
				{
					User = TestConfig.Username,
					Roles = new List<string> { "*" }
				})
				;

			try
			{
				// Get all tokens and confirm we got one
				authenticationTokens = await RundeckClient
				.AuthenticationTokens
				.GetAllAsync()
				;

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
			}
			finally
			{
				// Cleanup token
				await RundeckClient
					.AuthenticationTokens
					.DeleteAsync(newToken.Id)
					;
			}

			authenticationTokens = await RundeckClient
				.AuthenticationTokens
				.GetAllAsync()
				;

			authenticationTokens.Should().NotBeNull();
			authenticationTokens.Should().BeEmpty();
		}

		[Fact]
		public async Task AuthenticationTokens_GetAllByUserAsync_Passes()
		{
			var authenticationTokens = await RundeckClient
				.AuthenticationTokens
				.GetAllByUserAsync(TestConfig.Username)
				;
			authenticationTokens.Should().NotBeNull();
			authenticationTokens.Should().BeEmpty();

			// Create
			var newToken = await RundeckClient
				.AuthenticationTokens
				.CreateAsync(new AuthenticationTokenCreationDto
				{
					User = TestConfig.Username,
					Roles = new List<string> { "*" }
				})
				;

			try
			{
				// Re-fetch the tokens by user
				authenticationTokens = await RundeckClient
				.AuthenticationTokens
				.GetAllByUserAsync(TestConfig.Username)
				;

				authenticationTokens.Should().NotBeNullOrEmpty();

				authenticationTokens[0].Id.Should().Be(newToken.Id);
				authenticationTokens[0].Token.Should().BeEmpty();
			}
			finally
			{
				// Delete
				await RundeckClient
					.AuthenticationTokens
					.DeleteAsync(newToken.Id)
					;
			}
		}

		[Fact]
		public async Task CreateDelete_Succeeds()
		{
			// Create
			var newToken = await RundeckClient
				.AuthenticationTokens
				.CreateAsync(new AuthenticationTokenCreationDto
				{
					User = TestConfig.Username,
					Roles = new List<string> { "*" }
				})
				;

			try
			{
				newToken.Should().NotBeNull();
				newToken.Id.Should().NotBeNull();
				newToken.User.Should().Be(TestConfig.Username);
				newToken.Creator.Should().Be(TestConfig.Username);
				newToken.Expired.Should().BeFalse();
				newToken.Expiration.Should().BeAfter(DateTimeOffset.UtcNow);
				newToken.Roles.Should().NotBeNullOrEmpty();
				newToken.Token.Should().NotBeNullOrEmpty();
			}
			finally
			{
				// Delete
				await RundeckClient
					.AuthenticationTokens
					.DeleteAsync(newToken.Id)
					;
			}
		}

		[Fact]
		public async Task Get_ExistingToken_Succeeds()
		{
			// Create
			var newToken = await RundeckClient
				.AuthenticationTokens
				.CreateAsync(new AuthenticationTokenCreationDto
				{
					User = TestConfig.Username,
					Roles = new List<string> { "*" }
				})
				;

			try
			{
				var existingToken = await RundeckClient
				.AuthenticationTokens
				.GetAsync(newToken.Id)
				;

				existingToken.Should().NotBeNull();
				existingToken.Id.Should().Be(newToken.Id);
				existingToken.User.Should().Be(TestConfig.Username);
				existingToken.Creator.Should().Be(TestConfig.Username);
				existingToken.Expired.Should().BeFalse();
				existingToken.Expiration.Should().Be(newToken.Expiration);
				existingToken.Roles.Should().NotBeEmpty();
				existingToken.Roles.Should().BeEquivalentTo(newToken.Roles);
				existingToken.Token.Should().NotBeEmpty();
				existingToken.Token.Should().Be(newToken.Token);
			}
			finally
			{
				// Delete
				await RundeckClient
					.AuthenticationTokens
					.DeleteAsync(newToken.Id)
					;
			}
		}
	}
}
