using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Rundeck info
	/// </summary>
	public class RundeckInfo
	{
		/// <summary>
		/// Version
		/// </summary>
		[JsonPropertyName("version")]
		public string Version { get; set; } = default!;

		/// <summary>
		/// Build
		/// </summary>
		[JsonPropertyName("build")]
		public string Build { get; set; } = default!;

		/// <summary>
		/// BuildGit
		/// </summary>
		[JsonPropertyName("buildGit")]
		public string BuildGit { get; set; } = default!;

		/// <summary>
		/// Node
		/// </summary>
		[JsonPropertyName("node")]
		public string Node { get; set; } = default!;

		/// <summary>
		/// Base
		/// </summary>
		[JsonPropertyName("base")]
		public string Base { get; set; } = default!;

		/// <summary>
		/// API version
		/// </summary>
		[JsonPropertyName("apiversion")]
		public int ApiVersion { get; set; }

		/// <summary>
		/// ServerUUID
		/// </summary>
		[JsonPropertyName("serverUUID")]
		public string ServerUUID { get; set; } = default!;
	}
}