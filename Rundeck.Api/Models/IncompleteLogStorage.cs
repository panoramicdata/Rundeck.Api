using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Incomplete log storage
	/// </summary>
	public class IncompleteLogStorage
	{
		/// <summary>
		/// The total
		/// </summary>
		[JsonPropertyName("total")]
		public int Total { get; set; }

		/// <summary>
		/// The max
		/// </summary>
		[JsonPropertyName("max")]
		public int Max { get; set; }

		/// <summary>
		/// The offset
		/// </summary>
		[JsonPropertyName("offset")]
		public int Offset { get; set; }

		/// <summary>
		/// The executions
		/// </summary>
		[JsonPropertyName("executions")]
		public List<Execution> Executions { get; set; } = new List<Execution>();
	}
}
