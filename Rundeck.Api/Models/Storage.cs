using System;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// A storage
	/// </summary>
	public class Storage
	{
		/// <summary>
		/// True if all local files (rdlog and state.json)
		/// are available for upload. False if one of them is not present on disk.
		/// </summary>
		[JsonPropertyName("localFilesPresent")]
		public bool LocalFilesPresent { get; set; }

		/// <summary>
		/// Comma-separated list of filetypes which have not be uploaded,
		/// e.g. rdlog,state.json. Types are rdlog (log output), state.json (workflow state data),
		/// execution.xml (execution definition)
		/// </summary>
		[JsonPropertyName("incompleteFiletypes")]
		public string IncompleteFileTypes { get; set; } = string.Empty;

		/// <summary>
		/// True if the log data storage is queued to be processed.
		/// </summary>
		[JsonPropertyName("queued")]
		public bool Queued { get; set; }

		/// <summary>
		/// True if the log data storage was processed but failed without completion.
		/// </summary>
		[JsonPropertyName("failed")]
		public bool Failed { get; set; }

		/// <summary>
		/// Date when log data storage was first processed. (W3C date format.)
		/// </summary>
		[JsonPropertyName("date")]
		public DateTimeOffset Date { get; set; }
	}
}