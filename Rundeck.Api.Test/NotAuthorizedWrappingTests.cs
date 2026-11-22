using System.Threading.Tasks;
using Xunit;

namespace Rundeck.Api.Test
{
	/// <summary>Refit wraps what the HTTP handler throws, so a 403 reaches callers as the inner exception.</summary>
	public class NotAuthorizedWrappingTests
	{
		[Fact]
		public async Task Forbidden_ThrowsNotAuthorizedAsInnerException()
		{
			using var server = new StubRundeckServer("{\"error\":true,\"message\":\"denied\"}", 403);
			using var client = server.CreateClient();

			await NotAuthorizedAssertions.ShouldThrowNotAuthorizedAsync(
				() => client.WebHooks.SendEventAsync("invalid_auth_token", TestContext.Current.CancellationToken));
		}
	}
}