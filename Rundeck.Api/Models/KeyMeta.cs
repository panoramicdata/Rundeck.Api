using System;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class KeyMeta
	{
		[JsonPropertyName("Rundeck-key-type")]
		public KeyType RundeckKeyType { get; set; } = default!;

		[JsonPropertyName("Rundeck-content-mask")]
		public string RundeckContentMask { get; set; } = default!;

		[JsonPropertyName("Rundeck-content-size")]
		public string RundeckContentSize { get; set; } = default!;

		[JsonPropertyName("Rundeck-content-type")]
		public string RundeckContentType { get; set; } = default!;

		[JsonPropertyName("Rundeck-content-creation-time")]
		public DateTime RundeckContentCreationTime { get; set; }

		[JsonPropertyName("Rundeck-content-modify-time")]
		public DateTime RundeckContentModifyTime { get; set; }

		[JsonPropertyName("Rundeck-auth-created-username")]
		public string RundeckAuthCreatedUsername { get; set; } = default!;

		[JsonPropertyName("Rundeck-auth-modified-username")]
		public string RundeckAuthModifiedUsername { get; set; } = default!;
	}
}
