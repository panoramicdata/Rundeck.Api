using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class Config
	{
		[JsonPropertyName("project.description")]
		public string ProjectDescription { get; set; } = default!;

		[JsonPropertyName("service.FileCopier.default.provider")]
		public string ServiceFileCopierDefaultProvider { get; set; } = default!;

		[JsonPropertyName("project.ssh-keypath")]
		public string ProjectSshKeypath { get; set; } = default!;

		[JsonPropertyName("service.NodeExecutor.default.provider")]
		public string ServiceNodeExecutorDefaultProvider { get; set; } = default!;

		[JsonPropertyName("project.name")]
		public string ProjectName { get; set; } = default!;

		[JsonPropertyName("project.ssh-authentication")]
		public string ProjectSshAuthentication { get; set; } = default!;

		[JsonPropertyName("resources.source.1.type")]
		public string ResourcesSource1Type { get; set; } = default!;
	}
}