using System;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class JobOptionFile
	{
		[JsonPropertyName("id")]
		public string Id { get; set; } = default!;

		[JsonPropertyName("user")]
		public string User { get; set; } = default!;

		[JsonPropertyName("optionName")]
		public string OptionName { get; set; } = default!;

		[JsonPropertyName("fileState")]
		public string FileState { get; set; } = default!;

		[JsonPropertyName("sha")]
		public string Sha { get; set; } = default!;

		[JsonPropertyName("jobId")]
		public string JobId { get; set; } = default!;

		[JsonPropertyName("dateCreated")]
		public DateTimeOffset DateCreated { get; set; }

		[JsonPropertyName("serverNodeUUID")]
		public string ServerNodeUUID { get; set; } = default!;

		[JsonPropertyName("fileName")]
		public object FileName { get; set; } = default!;

		[JsonPropertyName("size")]
		public int Size { get; set; }

		[JsonPropertyName("expirationDate")]
		public DateTimeOffset ExpirationDate { get; set; }

		[JsonPropertyName("execId")]
		public object ExecId { get; set; } = default!;
	}
}