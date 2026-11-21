using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// A CPU info
	/// </summary>
	public class CpuInfo
	{
		/// <summary>
		/// The loadAverage
		/// </summary>
		[JsonPropertyName("loadAverage")]
		public LoadAverage LoadAverage { get; set; } = new LoadAverage();

		/// <summary>
		/// The number of processors
		/// </summary>
		[JsonPropertyName("processors")]
		public int Processors { get; set; }
	}
}