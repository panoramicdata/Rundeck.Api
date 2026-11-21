using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class JobDeletionResult
	{
		[JsonPropertyName("id")]
		public string Id { get; set; } = default!;

		[JsonPropertyName("message")]
		public string Message { get; set; } = default!;

		[JsonPropertyName("errorCode")]
		public string? ErrorCode { get; set; }
	}
}
