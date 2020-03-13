using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Rundeck.Api.Test.Documented
{
	public class LogStorageTests : TestBase
	{
		public LogStorageTests(ITestOutputHelper output) : base(output)
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

		[Fact]
		public async void System_GetLogStorageIncompleteAsync_Passes()
		{
			var logStorage = await RundeckClient
				.System
				.GetIncompleteLogStorageAsync()
				.ConfigureAwait(false);

			logStorage.Should().NotBeNull();
		}

		[Fact]
		public async void System_ResumeIncompleteLogStorageAsync_Passes()
		{
			var response = await RundeckClient
				.System
				.ResumeIncompleteLogStorageAsync()
				.ConfigureAwait(false);

			response.Resumed.Should().BeTrue();
		}
	}
}
