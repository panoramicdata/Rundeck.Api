using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// LoadAverage
	/// </summary>
	public class LoadAverage
	{
		/// <summary>
		/// Unit
		/// </summary>
		[JsonPropertyName("unit")]
		public string Unit { get; set; } = default!;

		/// <summary>
		/// Average
		/// </summary>
		[JsonPropertyName("average")]
		public double Average { get; set; }
	}
}