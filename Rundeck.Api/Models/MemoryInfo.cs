using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// MemoryInfo
	/// </summary>
	public class MemoryInfo
	{
		/// <summary>
		/// Unit
		/// </summary>
		[JsonPropertyName("unit")]
		public string Unit { get; set; } = default!;

		/// <summary>
		/// Max value
		/// </summary>
		[JsonPropertyName("max")]
		public long Max { get; set; }

		/// <summary>
		/// Free
		/// </summary>
		[JsonPropertyName("free")]
		public long Free { get; set; }

		/// <summary>
		/// Total
		/// </summary>
		[JsonPropertyName("total")]
		public long Total { get; set; }
	}
}