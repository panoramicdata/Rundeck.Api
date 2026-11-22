using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// JvmInfo
	/// </summary>
	public class JvmInfo
	{
		/// <summary>
		/// Name
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; } = default!;

		/// <summary>
		/// Vendor
		/// </summary>
		[JsonPropertyName("vendor")]
		public string Vendor { get; set; } = default!;

		/// <summary>
		/// Version
		/// </summary>
		[JsonPropertyName("version")]
		public string Version { get; set; } = default!;

		/// <summary>
		/// Implementation version
		/// </summary>
		[JsonPropertyName("implementationVersion")]
		public string ImplementationVersion { get; set; } = default!;
	}
}