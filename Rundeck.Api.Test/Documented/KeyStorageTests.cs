using AwesomeAssertions;
using Rundeck.Api.Models;
using System.Threading.Tasks;
using Xunit;

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
				.GetAsync("", TestContext.Current.CancellationToken);

			keys.Should().NotBeNull();
			keys.Resources.Should().NotBeNull().And.BeEmpty();
		}

		[Fact]
		public async Task CreatePrivateKey_Passes()
		{
			var newKey = await RundeckClient
				.Keys
				.CreatePrivateKeyAsync("myprivatekey", "SomePrivateKeyText", TestContext.Current.CancellationToken);

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
					.DeleteAsync("myprivatekey", TestContext.Current.CancellationToken);
			}
		}

		[Fact]
		public async Task CreatePublicKey_Passes()
		{
			var newKey = await RundeckClient
				.Keys
				.CreatePublicKeyAsync("mypublickey", "SomePublicKeyText", TestContext.Current.CancellationToken);

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
					.DeleteAsync("mypublickey", TestContext.Current.CancellationToken);
			}
		}

		[Fact]
		public async Task CreatePasswordKey_Passes()
		{
			var newKey = await RundeckClient
				.Keys
				.CreatePasswordAsync("mypasswordkey", "SomepasswordKeyText", TestContext.Current.CancellationToken);

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
					.DeleteAsync("mypasswordkey", TestContext.Current.CancellationToken);
			}
		}
	}
}
