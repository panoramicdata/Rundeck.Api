using FluentAssertions;
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

			// Refetch the first token
			var tokens = await RundeckClient
				.AuthenticationTokens
				.GetAsync(authenticationTokens[0].Id)
				.ConfigureAwait(false);

			tokens.Should().NotBeNull();
			tokens.Should().NotBeEmpty();
			tokens.Should().ContainSingle();
			tokens[0].Id.Should().Be(authenticationTokens[0].Id);
		}
	}
}
