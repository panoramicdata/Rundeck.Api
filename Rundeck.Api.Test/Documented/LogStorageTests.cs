using AwesomeAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Rundeck.Api.Test.Documented
{
	public class LogStorageTests : TestBase
	{
		public LogStorageTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async Task System_GetLogStorageAsync_Passes()
		{
			var logStorage = await RundeckClient
				.System
				.GetLogStorageAsync(TestContext.Current.CancellationToken);

			logStorage.Should().NotBeNull();
		}

		[Fact]
		public async Task System_GetLogStorageIncompleteAsync_Passes()
		{
			var logStorage = await RundeckClient
				.System
				.GetIncompleteLogStorageAsync(TestContext.Current.CancellationToken);

			logStorage.Should().NotBeNull();
		}

		[Fact]
		public async Task System_ResumeIncompleteLogStorageAsync_Passes()
		{
			var response = await RundeckClient
				.System
				.ResumeIncompleteLogStorageAsync(TestContext.Current.CancellationToken);

			response.Resumed.Should().BeTrue();
		}
	}
}
