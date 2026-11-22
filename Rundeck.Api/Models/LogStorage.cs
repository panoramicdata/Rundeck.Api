using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// A log storage
	/// </summary>
	public class LogStorage
	{
		/// <summary>
		/// Whether it is enabled
		/// </summary>
		[JsonPropertyName("enabled")]
		public bool Enabled { get; set; }

		/// <summary>
		/// The plugin name
		/// </summary>
		[JsonPropertyName("pluginName")]
		public string PluginName { get; set; } = string.Empty;

		/// <summary>
		/// The succeeded count
		/// </summary>
		[JsonPropertyName("succeededCount")]
		public int SucceededCount { get; set; }

		/// <summary>
		/// The failed count
		/// </summary>
		[JsonPropertyName("failedCount")]
		public int FailedCount { get; set; }

		/// <summary>
		/// The queued count
		/// </summary>
		[JsonPropertyName("queuedCount")]
		public int QueuedCount { get; set; }

		/// <summary>
		/// The queued request count
		/// </summary>
		[JsonPropertyName("queuedRequestCount")]
		public int QueuedRequestCount { get; set; }

		/// <summary>
		/// The queued retries count
		/// </summary>
		[JsonPropertyName("queuedRetriesCount")]
		public int QueuedRetriesCount { get; set; }

		/// <summary>
		/// The queued incomplete count
		/// </summary>
		[JsonPropertyName("queuedIncompleteCount")]
		public int QueuedIncompleteCount { get; set; }

		/// <summary>
		/// The total count
		/// </summary>
		[JsonPropertyName("totalCount")]
		public int TotalCount { get; set; }

		/// <summary>
		/// The incomplete count
		/// </summary>
		[JsonPropertyName("incompleteCount")]
		public int IncompleteCount { get; set; }

		/// <summary>
		/// The missing count
		/// </summary>
		[JsonPropertyName("missingCount")]
		public int MissingCount { get; set; }

		/// <summary>
		/// The retries count
		/// </summary>
		[JsonPropertyName("retriesCount")]
		public int RetriesCount { get; set; }
	}
}
