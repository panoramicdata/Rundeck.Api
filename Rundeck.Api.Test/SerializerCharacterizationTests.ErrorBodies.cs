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
			var thrown = await ThrownForUserGetAsync<NotAuthorizedException>("""{"error":true,"apiversion":34,"errorCode":"api.error.unauthorized","message":"nope"}""", 403);

			thrown.Message.Should().Be("nope");
			thrown.RundeckError!.ErrorCode.Should().Be("api.error.unauthorized");
			thrown.RundeckError.ApiVersion.Should().Be(34);
			thrown.RundeckError.Error.Should().BeTrue();
		}

		[Fact]
		public async Task Forbidden_WithUnknownErrorFields_StillThrowsNotAuthorized()
		{
			(await ThrownForUserGetAsync<NotAuthorizedException>("""{"error":true,"message":"nope","extra":{"a":1},"messages":[]}""", 403)).Message.Should().Be("nope");
		}

		[Fact]
		public async Task BadRequest_ThrowsRundeckExceptionWithTheErrorMessage()
		{
			(await ThrownForUserGetAsync<RundeckException>("""{"error":true,"message":"bad"}""", 400)).Message.Should().Be("bad");
		}

		[Fact]
		public async Task Forbidden_WithNullBody_FallsBackToTheNoContentMessage()
		{
			(await ThrownForUserGetAsync<NotAuthorizedException>("null", 403)).Message.Should().Be("No content body.");
		}

		[Fact]
		public async Task ServerError_ThrowsRundeckExceptionWithTheRawBody()
		{
			(await ThrownForUserGetAsync<RundeckException>("boom", 500)).Message.Should().Be("boom");
		}

		[Fact]
		public async Task NotFound_ThrowsApiException()
		{
			await ThrownForUserGetAsync<ApiException>("{}", 404);
		}
	}
}
