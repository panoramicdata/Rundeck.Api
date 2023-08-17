using FluentAssertions;
using Rundeck.Api.Models;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Rundeck.Api.Test.Documented
{
	public class KeyStorageTests : TestBase
	{
		public KeyStorageTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async Task GetPathAsync_Root_Passes()
		{
			// Get the keys from the root
			var keys = await RundeckClient
				.Keys
				.GetAsync("")
				.ConfigureAwait(false);

			keys.Should().NotBeNull();
			keys.Resources.Should().NotBeNull().And.BeEmpty();
		}

		[Fact]
		public async Task CreatePrivateKey_Passes()
		{
			var newKey = await RundeckClient
				.Keys
				.CreatePrivateKeyAsync("myprivatekey", "SomePrivateKeyText")
				.ConfigureAwait(false);

			try
			{
				newKey.Should().NotBeNull();
				newKey.Type.Should().Be(KeyResourceType.File);
				newKey.Meta.Should().NotBeNull();
				newKey.Meta.RundeckKeyType.Should().Be(KeyType.Private);
				newKey.Resources.Should().BeEmpty();
			}
			finally
			{
				// Cleanup
				await RundeckClient
					.Keys
					.DeleteAsync("myprivatekey")
					.ConfigureAwait(false);
			}
		}

		[Fact]
		public async Task CreatePublicKey_Passes()
		{
			var newKey = await RundeckClient
				.Keys
				.CreatePublicKeyAsync("mypublickey", "SomePublicKeyText")
				.ConfigureAwait(false);

			try
			{
				newKey.Should().NotBeNull();
				newKey.Type.Should().Be(KeyResourceType.File);
				newKey.Meta.Should().NotBeNull();
				newKey.Meta.RundeckKeyType.Should().Be(KeyType.Public);
				newKey.Resources.Should().BeEmpty();
			}
			finally
			{
				// Cleanup
				await RundeckClient
					.Keys
					.DeleteAsync("mypublickey")
					.ConfigureAwait(false);
			}
		}

		[Fact]
		public async Task CreatePasswordKey_Passes()
		{
			var newKey = await RundeckClient
				.Keys
				.CreatePasswordAsync("mypasswordkey", "SomepasswordKeyText")
				.ConfigureAwait(false);

			try
			{
				newKey.Should().NotBeNull();
				newKey.Type.Should().Be(KeyResourceType.File);
				newKey.Meta.Should().NotBeNull();
				newKey.Meta.RundeckKeyType.Should().Be(KeyType.Password);
				newKey.Resources.Should().BeEmpty();
			}
			finally
			{
				// Cleanup
				await RundeckClient
					.Keys
					.DeleteAsync("mypasswordkey")
					.ConfigureAwait(false);
			}
		}
	}
}
