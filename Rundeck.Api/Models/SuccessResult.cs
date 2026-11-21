using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class SuccessResult
	{
		[JsonPropertyName("success")]
		public bool Success { get; set; }
	}
}