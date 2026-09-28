using AwesomeAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Rundeck.Api.Test.Documented
{
	public class SystemInfoTests : TestBase
	{
		public SystemInfoTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async Task GetSystemInfoAsync_PassesAsync()
		{
			var systemInfo = await RundeckClient
				.System
				.GetSystemInfoAsync(TestContext.Current.CancellationToken);

			systemInfo.Should().NotBeNull();
			// Todo - Add assertions to check systemInfo properties
		}
	}
}
