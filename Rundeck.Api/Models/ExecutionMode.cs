using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// ExecutionMode
	/// </summary>
	public class ExecutionMode
	{
		/// <summary>
		/// The execution mode
		/// </summary>
		[JsonPropertyName("executionMode")]
		public ExecutionModeEnum ExecutionModeEnum { get; set; } = default;
	}
}
