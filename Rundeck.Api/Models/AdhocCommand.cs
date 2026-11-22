using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class AdhocCommand
	{
		/// <summary>
		/// Name of the Project
		/// </summary>
		[JsonPropertyName("project")]
		public string Project { get; set; } = default!;

		/// <summary>
		/// The shell command string to run
		/// </summary>
		[JsonPropertyName("exec")]
		public string Exec { get; set; } = default!;

		/// <summary>
		/// Threadcount to use
		/// </summary>
		[JsonPropertyName("nodeThreadcount")]
		public int? NodeThreadcount { get; set; }

		/// <summary>
		/// Continue executing on other nodes even if some fail
		/// </summary>
		[JsonPropertyName("nodeKeepgoing")]
		public bool? NodeKeepgoing { get; set; }

		/// <summary>
		/// The User who ran the command
		/// </summary>
		[JsonPropertyName("asUser")]
		public string? AsUser { get; set; } = default!;

		[JsonPropertyName("filter")]
		public string? Filter { get; set; } = default!;
	}
}