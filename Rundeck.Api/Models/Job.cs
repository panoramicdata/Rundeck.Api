using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// A job
	/// </summary>
	[DataContract]
	public class Job
	{
		[DataMember(Name = "id")]
		// Todo: this should be UUID
		public string Id { get; set; } = default!;

		[DataMember(Name = "name")]
		public string Name { get; set; } = default!;

		[DataMember(Name = "group")]
		public Group Group { get; set; } = new Group();

		[DataMember(Name = "project")]
		public Project Project { get; set; } = new Project();

		[DataMember(Name = "description")]
		public string Description { get; set; } = default!;

		[DataMember(Name = "href")]
		public string Href { get; set; } = default!;

		[DataMember(Name = "permalink")]
		public string Permalink { get; set; } = default!;

		[DataMember(Name = "scheduled")]
		public bool Scheduled { get; set; }

		[DataMember(Name = "scheduleEnabled")]
		public bool ScheduleEnabled { get; set; }

		[DataMember(Name = "enabled")]
		public bool Enabled { get; set; }
	}
}
