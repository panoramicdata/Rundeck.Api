using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Rundeck.Api.Test
{
	public class SystemTests : TestBase
	{
		public SystemTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async void System_GetLogStorageAsync_Passes()
		{
			var logStorage = await RundeckClient
				.System
				.GetLogStorageAsync()
				.ConfigureAwait(false);

			logStorage.Should().NotBeNull();
		}
	}
}
