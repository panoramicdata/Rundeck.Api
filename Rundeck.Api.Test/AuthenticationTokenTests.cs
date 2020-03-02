using FluentAssertions;
using Rundeck.Api.Models;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Rundeck.Api.Test
{
	public class AuthenticationTokenTests : TestBase
	{
		public AuthenticationTokenTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async void AuthenticationTokens_GetAllAsync_Passes()
		{
			var authenticationTokens = await RundeckClient
				.AuthenticationTokens
				.GetAllAsync()
				.ConfigureAwait(false);

			authenticationTokens.Should().NotBeNullOrEmpty();

			foreach (var token in authenticationTokens)
			{
				token.User.Should().Be(TestConfig.Username);
				token.Creator.Should().Be(TestConfig.Username);
				token.Expired.Should().BeFalse();
				token.Expiration.Should().BeAfter(DateTimeOffset.UtcNow);
				token.Roles.Should().NotBeEmpty();
			}
		}

		[Fact]
		public async void AuthenticationTokens_GetAllByUserAsync_Passes()
		{
			var authenticationTokens = await RundeckClient
				.AuthenticationTokens
				.GetAllByUserAsync(TestConfig.Username)
				.ConfigureAwait(false);
			authenticationTokens.Should().NotBeNullOrEmpty();

			// Re-fetch the first token
			var token = await RundeckClient
				.AuthenticationTokens
				.GetAsync(authenticationTokens[0].Id)
				.ConfigureAwait(false);

			token.Should().NotBeNull();
			token.Id.Should().Be(authenticationTokens[0].Id);
			token.Token.Should().NotBeNullOrWhiteSpace();
		}

		/// <summary>
		/// Broken test.  See https://github.com/rundeck/rundeck/issues/5842
		/// </summary>
		[Fact(Skip = "Issue 5842")]
		//[Fact]
		public async void CreateUpdateDelete_Succeeds()
		{
			// Create
			var newToken = await RundeckClient
				.AuthenticationTokens
				.CreateAsync(new AuthenticationTokenCreationDto
				{
					User = TestConfig.Username,
				})
				.ConfigureAwait(false);

			newToken.Should().NotBeNull();
			newToken.Id.Should().NotBeNull();

			// Delete
			await RundeckClient
				.AuthenticationTokens
				.DeleteAsync(newToken.Id)
				.ConfigureAwait(false);
		}
	}
}
