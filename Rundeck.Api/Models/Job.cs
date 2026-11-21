using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// A job
	/// </summary>
	public class Job : JobReferenceBase
	{
		[JsonPropertyName("description")]
		public string Description { get; set; } = default!;

		[JsonPropertyName("options")]
		public Dictionary<string, string> Options { get; set; } = new Dictionary<string, string>();

		[JsonPropertyName("scheduled")]
		public bool Scheduled { get; set; }

		[JsonPropertyName("scheduleEnabled")]
		public bool ScheduleEnabled { get; set; }

		[JsonPropertyName("enabled")]
		public bool Enabled { get; set; }

		// provided in cluster mode
		[JsonPropertyName("serverOwner")]
		public bool? ServerOwner { get; set; }

		[JsonPropertyName("serverNodeUUID")]
		public string? ServerNodeUUID { get; set; } = default!;

		[JsonPropertyName("averageDuration")]
		public int AverageDuration { get; set; }
	}
}
