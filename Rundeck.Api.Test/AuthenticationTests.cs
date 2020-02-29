using Xunit;
using Xunit.Abstractions;

namespace Rundeck.Api.Test
{
	public class AuthenticationTests : TestBase
	{
		public AuthenticationTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public void BlankTest_Passes()
		{
		}
	}
}
