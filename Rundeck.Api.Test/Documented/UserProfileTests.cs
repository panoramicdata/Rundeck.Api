﻿using FluentAssertions;
using Rundeck.Api.Models;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Rundeck.Api.Test.Documented
{
	public class UserProfileTests : TestBase
	{
		public UserProfileTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async Task Users_GetAllAsync_Passes()
		{
			var users = await RundeckClient
				.Users
				.GetAllAsync()
				.ConfigureAwait(false);

			users.Should().NotBeNullOrEmpty();
		}

		[Fact]
		public async Task Users_GetMeAsync_Passes()
		{
			var user = await RundeckClient
				.Users
				.GetMeAsync()
				.ConfigureAwait(false);

			user.Should().NotBeNull();
		}

		[Fact]
		public async Task Users_GetAsync_Passes()
		{
			var users = await RundeckClient
				.Users
				.GetAllAsync()
				.ConfigureAwait(false);

			users.Should().NotBeNullOrEmpty();

			var user = await RundeckClient
				.Users
				.GetAsync(users[0].Login)
				.ConfigureAwait(false);

			user.Should().NotBeNull();
			user.Login.Should().BeEquivalentTo(users[0].Login);
		}

		[Fact]
		public async Task Users_GetMyRoleSetAsync_Passes()
		{
			var roles = await RundeckClient
				.Users
				.GetMyRoleSetAsync()
				.ConfigureAwait(false);

			roles.Should().NotBeNull();
			roles.Roles.Should().NotBeNullOrEmpty();
		}

		[Fact]
		public async Task Users_UpdateAsync_Passes()
		{
			var users = await RundeckClient
				.Users
				.GetAllAsync()
				.ConfigureAwait(false);

			users.Should().NotBeNullOrEmpty();

			var user = await RundeckClient
				.Users
				.GetAsync(users[0].Login)
				.ConfigureAwait(false);

			var updatedUser = await RundeckClient
			.Users
			.UpdateAsync(user.Login, new User
			{
				FirstName = "John",
				LastName = "Smith",
				Email = "john.smith@example.com"
			})
			.ConfigureAwait(false);

			updatedUser.Should().NotBeNull();
			updatedUser.Login.Should().Be(user.Login);
			updatedUser.FirstName.Should().Be("John");
			updatedUser.LastName.Should().Be("Smith");
			updatedUser.Email.Should().Be("john.smith@example.com");
		}
	}
}
