using AwesomeAssertions;
using Rundeck.Api.Models;
using System;
using System.Threading;
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
			var keys = await RundeckClient
				.Keys
				.GetAsync("", TestContext.Current.CancellationToken);

			keys.Should().NotBeNull();
			keys.Resources.Should().NotBeNull().And.BeEmpty();
		}

		[Fact]
		public Task CreatePrivateKey_Passes()
			=> AssertKeyCreatedAsync("myprivatekey", KeyType.Private, RundeckClient.Keys.CreatePrivateKeyAsync);

		[Fact]
		public Task CreatePublicKey_Passes()
			=> AssertKeyCreatedAsync("mypublickey", KeyType.Public, RundeckClient.Keys.CreatePublicKeyAsync);

		[Fact]
		public Task CreatePasswordKey_Passes()
			=> AssertKeyCreatedAsync("mypasswordkey", KeyType.Password, RundeckClient.Keys.CreatePasswordAsync);

		private async Task AssertKeyCreatedAsync(
			string name,
			KeyType expectedType,
			Func<string, string, CancellationToken, Task<KeyStorage>> create)
		{
			var newKey = await create(name, "SomeKeyText", TestContext.Current.CancellationToken);

			try
			{
				newKey.Should().NotBeNull();
				newKey.Type.Should().Be(KeyResourceType.File);
				newKey.Meta.Should().NotBeNull();
				newKey.Meta.RundeckKeyType.Should().Be(expectedType);
				newKey.Resources.Should().BeEmpty();
			}
			finally
			{
				await RundeckClient
					.Keys
					.DeleteAsync(name, TestContext.Current.CancellationToken);
			}
		}
	}
}
