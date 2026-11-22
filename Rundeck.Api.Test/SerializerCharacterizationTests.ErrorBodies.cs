using AwesomeAssertions;
using Refit;
using Rundeck.Api.Exceptions;
using System.Threading.Tasks;
using Xunit;
using static Rundeck.Api.Test.SerializerTestHelpers;

namespace Rundeck.Api.Test
{
	public partial class SerializerCharacterizationTests
	{
		[Fact]
		public async Task Forbidden_ThrowsNotAuthorizedWithTheErrorMessage()
		{
			using var server = new StubRundeckServer("""{"error":true,"apiversion":34,"errorCode":"api.error.unauthorized","message":"nope"}""", 403);
			using var client = server.CreateClient();

			var act = () => client.Users.GetAsync("u", TestContext.Current.CancellationToken);

			var thrown = await ThrownAsync<NotAuthorizedException>(act);
			thrown.Message.Should().Be("nope");
			thrown.RundeckError!.ErrorCode.Should().Be("api.error.unauthorized");
			thrown.RundeckError.ApiVersion.Should().Be(34);
			thrown.RundeckError.Error.Should().BeTrue();
		}

		[Fact]
		public async Task Forbidden_WithUnknownErrorFields_StillThrowsNotAuthorized()
		{
			using var server = new StubRundeckServer("""{"error":true,"message":"nope","extra":{"a":1},"messages":[]}""", 403);
			using var client = server.CreateClient();

			var act = () => client.Users.GetAsync("u", TestContext.Current.CancellationToken);

			(await ThrownAsync<NotAuthorizedException>(act)).Message.Should().Be("nope");
		}

		[Fact]
		public async Task BadRequest_ThrowsRundeckExceptionWithTheErrorMessage()
		{
			using var server = new StubRundeckServer("""{"error":true,"message":"bad"}""", 400);
			using var client = server.CreateClient();

			var act = () => client.Users.GetAsync("u", TestContext.Current.CancellationToken);

			(await ThrownAsync<RundeckException>(act)).Message.Should().Be("bad");
		}

		[Fact]
		public async Task Forbidden_WithNullBody_FallsBackToTheNoContentMessage()
		{
			using var server = new StubRundeckServer("null", 403);
			using var client = server.CreateClient();

			var act = () => client.Users.GetAsync("u", TestContext.Current.CancellationToken);

			(await ThrownAsync<NotAuthorizedException>(act)).Message.Should().Be("No content body.");
		}

		[Fact]
		public async Task ServerError_ThrowsRundeckExceptionWithTheRawBody()
		{
			using var server = new StubRundeckServer("boom", 500);
			using var client = server.CreateClient();

			var act = () => client.Users.GetAsync("u", TestContext.Current.CancellationToken);

			(await ThrownAsync<RundeckException>(act)).Message.Should().Be("boom");
		}

		[Fact]
		public async Task NotFound_ThrowsApiException()
		{
			using var server = new StubRundeckServer("{}", 404);
			using var client = server.CreateClient();

			var act = () => client.Users.GetAsync("u", TestContext.Current.CancellationToken);

			await act.Should().ThrowAsync<ApiException>();
		}
	}
}
