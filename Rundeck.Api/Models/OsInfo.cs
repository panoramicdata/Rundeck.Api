using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Operating System Info
	/// </summary>
	public class OsInfo
	{
		/// <summary>
		/// Arch
		/// </summary>
		[JsonPropertyName("arch")]
		public string Arch { get; set; } = default!;

		/// <summary>
		/// Name
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; } = default!;

		/// <summary>
		/// Version
		/// </summary>
		[JsonPropertyName("version")]
		public string Version { get; set; } = default!;
	}
}