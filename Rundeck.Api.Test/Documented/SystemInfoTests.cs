using FluentAssertions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

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
				.GetSystemInfoAsync()
				.ConfigureAwait(false);

			systemInfo.Should().NotBeNull();
			// Todo - Add assertions to check systemInfo properties
		}
	}
}
