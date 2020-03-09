using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class ProjectResource
	{
		[DataMember(Name = "nodename")]
		public string Nodename { get; set; } = default!;

		[DataMember(Name = "hostname")]
		public string Hostname { get; set; } = default!;

		[DataMember(Name = "osFamily")]
		public string OsFamily { get; set; } = default!;

		[DataMember(Name = "osVersion")]
		public string OsVersion { get; set; } = default!;

		[DataMember(Name = "osArch")]
		public string OsArch { get; set; } = default!;

		[DataMember(Name = "description")]
		public string Description { get; set; } = default!;

		[DataMember(Name = "osName")]
		public string OsName { get; set; } = default!;

		[DataMember(Name = "username")]
		public string Username { get; set; } = default!;

		[DataMember(Name = "tags")]
		public string Tags { get; set; } = default!;
	}
}