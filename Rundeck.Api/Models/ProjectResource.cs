using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class ProjectResource
	{
		[JsonPropertyName("nodename")]
		public string Nodename { get; set; } = default!;

		[JsonPropertyName("hostname")]
		public string Hostname { get; set; } = default!;

		[JsonPropertyName("osFamily")]
		public string OsFamily { get; set; } = default!;

		[JsonPropertyName("osVersion")]
		public string OsVersion { get; set; } = default!;

		[JsonPropertyName("osArch")]
		public string OsArch { get; set; } = default!;

		[JsonPropertyName("description")]
		public string Description { get; set; } = default!;

		[JsonPropertyName("osName")]
		public string OsName { get; set; } = default!;

		[JsonPropertyName("username")]
		public string Username { get; set; } = default!;

		[JsonPropertyName("tags")]
		public string Tags { get; set; } = default!;
	}
}