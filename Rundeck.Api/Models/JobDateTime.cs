using System;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class JobDateTime
	{
		[JsonPropertyName("unixtime")]
		public long Unixtime { get; set; }

		[JsonPropertyName("date")]
		public DateTimeOffset Date { get; set; }
	}
}