using System.Runtime.Serialization;

namespace Rundeck.Api.Models.Dtos
{
	[DataContract]
	public class WebHookCreationDto
	{
		[DataMember(Name = "config")]
		public WebHookConfig Config { get; set; } = new WebHookConfig();

		[DataMember(Name = "enabled")]
		public bool Enabled { get; set; }

		[DataMember(Name = "eventPlugin")]
		public string EventPlugin { get; set; } = default!;

		[DataMember(Name = "name")]
		public string Name { get; set; } = default!;

		[DataMember(Name = "project")]
		public string Project { get; set; } = default!;

		[DataMember(Name = "roles")]
		public string Roles { get; set; } = default!;

		[DataMember(Name = "user")]
		public string User { get; set; } = default!;
	}
}