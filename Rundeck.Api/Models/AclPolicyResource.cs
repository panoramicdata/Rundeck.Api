using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// An AclPolicy when listing policies
	/// </summary>
	public class AclPolicyResource
	{
		/// <summary>
		/// Path
		/// </summary>
		[JsonPropertyName("path")]
		public string Path { get; set; } = default!;

		/// <summary>
		/// Name
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; } = default!;

		/// <summary>
		/// Type
		/// Todo: Should be an enum, gather possible values
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; } = default!;

		/// <summary>
		/// Href
		/// </summary>
		[JsonPropertyName("href")]
		public string Href { get; set; } = default!;
	}
}