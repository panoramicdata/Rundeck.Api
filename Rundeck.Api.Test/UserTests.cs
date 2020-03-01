using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Rundeck.Api.Test
{
	public class UserTests : TestBase
	{
		public UserTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async void Users_GetAllAsync_Passes()
		{
			var users = await RundeckClient
				.Users
				.GetAllAsync()
				.ConfigureAwait(false);

			users.Should().NotBeNull();
			users.Should().NotBeEmpty();
		}

		[Fact]
		public async void Users_GetMeAsync_Passes()
		{
			var user = await RundeckClient
				.Users
				.GetMeAsync()
				.ConfigureAwait(false);

			user.Should().NotBeNull();
		}

		[Fact]
		public async void Users_GetAsync_Passes()
		{
			var users = await RundeckClient
				.Users
				.GetAllAsync()
				.ConfigureAwait(false);

			var user = await RundeckClient
				.Users
				.GetAsync(users[0].Login)
				.ConfigureAwait(false);

			user.Should().NotBeNull();
			user.Login.Should().BeEquivalentTo(users[0].Login);
		}

		[Fact]
		public async void Users_GetMyRoleSetAsync_Passes()
		{
			var roles = await RundeckClient
				.Users
				.GetMyRoleSetAsync()
				.ConfigureAwait(false);

			roles.Should().NotBeNull();
			roles.Roles.Should().NotBeNullOrEmpty();
		}
	}
}
