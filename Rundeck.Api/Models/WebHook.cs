using Rundeck.Api.Models;
using System.Runtime.Serialization;

namespace Rundeck.Api.Interfaces
{
	[DataContract]
	public class WebHook
	{
		[DataMember(Name = "authToken")]
		public string AuthToken { get; set; } = default!;

		[DataMember(Name = "config")]
		public WebHookConfig Config { get; set; } = new WebHookConfig();

		[DataMember(Name = "creator")]
		public string Creator { get; set; } = default!;

		[DataMember(Name = "enabled")]
		public bool Enabled { get; set; }

		[DataMember(Name = "eventPlugin")]
		public string EventPlugin { get; set; } = default!;

		[DataMember(Name = "id")]
		public int Id { get; set; }

		[DataMember(Name = "uuid")]
		public string Uuid { get; set; } = default!;

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