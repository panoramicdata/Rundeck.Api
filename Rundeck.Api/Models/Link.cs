using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Link
	/// </summary>
	public class Link
	{
		/// <summary>
		/// The HREF
		/// </summary>
		[JsonPropertyName("href")]
		public string Href { get; set; } = default!;

		/// <summary>
		/// The ContentType
		/// </summary>
		[JsonPropertyName("contentType")]
		public string ContentType { get; set; } = default!;
	}
}
