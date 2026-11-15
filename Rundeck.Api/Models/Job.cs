using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// A job
	/// </summary>
	[DataContract]
	public class Job : JobReferenceBase
	{
		[DataMember(Name = "description")]
		public string Description { get; set; } = default!;

		[DataMember(Name = "options")]
		public Dictionary<string, string> Options { get; set; } = new Dictionary<string, string>();

		[DataMember(Name = "scheduled")]
		public bool Scheduled { get; set; }

		[DataMember(Name = "scheduleEnabled")]
		public bool ScheduleEnabled { get; set; }

		[DataMember(Name = "enabled")]
		public bool Enabled { get; set; }

		// provided in cluster mode
		[DataMember(Name = "serverOwner")]
		public bool? ServerOwner { get; set; }

		[DataMember(Name = "serverNodeUUID")]
		public string? ServerNodeUUID { get; set; } = default!;

		[DataMember(Name = "averageDuration")]
		public int AverageDuration { get; set; }
	}
}
