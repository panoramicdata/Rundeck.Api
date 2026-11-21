using System.Text.Json.Serialization;

namespace Rundeck.Api
{
	/// <summary>
	/// An API error, as sent back from the server
	/// </summary>
	[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Skip)]
	public class RundeckError
	{
		/// <summary>
		/// Whether this is an error
		/// </summary>
		[JsonPropertyName("error")]
		public bool Error { get; set; }

		/// <summary>
		/// The API version
		/// </summary>
		[JsonPropertyName("apiversion")]
		public int ApiVersion { get; set; }

		/// <summary>
		/// The error code
		/// </summary>
		[JsonPropertyName("errorCode")]
		public string ErrorCode { get; set; } = string.Empty;

		/// <summary>
		/// The message
		/// </summary>
		[JsonPropertyName("message")]
		public string Message { get; set; } = string.Empty;
	}
}