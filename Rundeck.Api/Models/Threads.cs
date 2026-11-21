using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Threads
	/// </summary>
	public class Threads
	{
		/// <summary>
		/// Active
		/// </summary>
		[JsonPropertyName("active")]
		public int Active { get; set; }
	}
}