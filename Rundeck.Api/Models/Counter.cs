using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Counter
	/// </summary>
	public class Counter
	{
		/// <summary>
		/// The count
		/// </summary>
		[JsonPropertyName("count")]
		public int Count { get; set; }
	}
}
