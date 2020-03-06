using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class Config
	{
		[DataMember(Name = "project.description")]
		public string ProjectDescription { get; set; } = default!;

		[DataMember(Name = "service.FileCopier.default.provider")]
		public string ServiceFileCopierDefaultProvider { get; set; } = default!;

		[DataMember(Name = "project.ssh-keypath")]
		public string ProjectSshKeypath { get; set; } = default!;

		[DataMember(Name = "service.NodeExecutor.default.provider")]
		public string ServiceNodeExecutorDefaultProvider { get; set; } = default!;

		[DataMember(Name = "project.name")]
		public string ProjectName { get; set; } = default!;

		[DataMember(Name = "project.ssh-authentication")]
		public string ProjectSshAuthentication { get; set; } = default!;

		[DataMember(Name = "resources.source.1.type")]
		public string ResourcesSource1Type { get; set; } = default!;
	}
}