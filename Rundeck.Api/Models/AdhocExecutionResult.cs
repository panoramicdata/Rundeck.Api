using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class AdhocExecutionResult
	{
		[JsonPropertyName("message")]
		public string Message { get; set; } = default!;

		[JsonPropertyName("execution")]
		public Execution Execution { get; set; } = new Execution();
	}
}
