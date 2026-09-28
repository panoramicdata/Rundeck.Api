using AwesomeAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Rundeck.Api.Test.Documented
{
	public class PluginsTests : TestBase
	{
		public PluginsTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async Task Plugins_GetAll_Passes()
		{
			var plugins = await RundeckClient
				.Plugins
				.GetAllAsync(TestContext.Current.CancellationToken);

			plugins.Should().NotBeNull();
		}
	}
}
