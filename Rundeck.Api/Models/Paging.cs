using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class Paging
	{
		[JsonPropertyName("offset")]
		public int Offset { get; set; }

		[JsonPropertyName("max")]
		public int Max { get; set; }

		[JsonPropertyName("total")]
		public int Total { get; set; }

		[JsonPropertyName("count")]
		public int Count { get; set; }
	}
}