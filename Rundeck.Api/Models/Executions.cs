using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Executions
	/// </summary>
	public class Executions
	{
		/// <summary>
		/// Execution status
		/// </summary>
		[JsonPropertyName("active")]
		public bool Active { get; set; }

		/// <summary>
		/// ExecutionMode
		/// </summary>
		[JsonPropertyName("executionMode")]
		public ExecutionModeEnum ExecutionMode { get; set; }
	}
}