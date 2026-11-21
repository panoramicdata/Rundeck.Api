using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// An execution
	/// </summary>
	public class Execution
	{
		/// <summary>
		/// Execution ID
		/// </summary>
		[JsonPropertyName("id")]
		public int Id { get; set; }

		/// <summary>
		/// Project Name
		/// </summary>
		[JsonPropertyName("project")]
		public string Project { get; set; } = string.Empty;

		/// <summary>
		/// API URL for Execution
		/// </summary>
		[JsonPropertyName("href")]
		public string Href { get; set; } = string.Empty;

		/// <summary>
		/// GUI URL for Execution
		/// </summary>
		[JsonPropertyName("permalink")]
		public string Permalink { get; set; } = string.Empty;

		/// <summary>
		/// The storage
		/// </summary>
		[JsonPropertyName("storage")]
		public Storage Storage { get; set; } = new Storage();

		/// <summary>
		/// The errors
		/// </summary>
		[JsonPropertyName("errors")]
		public List<string> Errors { get; set; } = new List<string>();

		/// <summary>
		/// Status
		/// </summary>
		[JsonPropertyName("status")]
		public JobExecutionStatus Status { get; set; } = default!;

		[JsonPropertyName("executionType")]
		public string ExecutionType { get; set; } = default!;

		[JsonPropertyName("user")]
		public string User { get; set; } = default!;

		[JsonPropertyName("date-started")]
		public JobDateTime DateStarted { get; set; } = new JobDateTime();

		[JsonPropertyName("date-ended")]
		public JobDateTime DateEnded { get; set; } = new JobDateTime();

		[JsonPropertyName("job")]
		public Job Job { get; set; } = new Job();

		[JsonPropertyName("description")]
		public string Description { get; set; } = default!;

		[JsonPropertyName("argstring")]
		public object Argstring { get; set; } = default!;

		[JsonPropertyName("serverUUID")]
		public string ServerUUID { get; set; } = default!;

		[JsonPropertyName("successfulNodes")]
		public IList<string> SuccessfulNodes { get; set; } = default!;

		/// <summary>
		/// Whether the execution's job has since been deleted
		/// </summary>
		[JsonPropertyName("jobDeleted")]
		public bool? JobDeleted { get; set; }
	}
}