using FluentAssertions;
using Rundeck.Api.Models;
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

			authenticationTokens.Should().NotBeNull();
			authenticationTokens.Should().NotBeEmpty();
		}

		[Fact]
		public async void AuthenticationTokens_GetAllByUserAsync_Passes()
		{
			var authenticationTokens = await RundeckClient
				.AuthenticationTokens
				.GetAllByUserAsync(TestConfig.Username)
				.ConfigureAwait(false);
			authenticationTokens.Should().NotBeNull();
			authenticationTokens.Should().NotBeEmpty();

			// Re-fetch the first token
			var token = await RundeckClient
				.AuthenticationTokens
				.GetAsync(authenticationTokens[0].Id)
				.ConfigureAwait(false);

			token.Should().NotBeNull();
			token.Id.Should().Be(authenticationTokens[0].Id);
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
