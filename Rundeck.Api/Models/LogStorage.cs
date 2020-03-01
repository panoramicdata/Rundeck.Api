using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// A log storage
	/// </summary>
	[DataContract]
	public class LogStorage
	{
		/// <summary>
		/// Whether it is enabled
		/// </summary>
		[DataMember(Name = "enabled")]
		public bool Enabled { get; set; }

		/// <summary>
		/// The plugin name
		/// </summary>
		[DataMember(Name = "pluginName")]
		public string PluginName { get; set; } = string.Empty;

		/// <summary>
		/// The succeeded count
		/// </summary>
		[DataMember(Name = "succeededCount")]
		public int SucceededCount { get; set; }

		/// <summary>
		/// The failed count
		/// </summary>
		[DataMember(Name = "failedCount")]
		public int FailedCount { get; set; }

		/// <summary>
		/// The queued count
		/// </summary>
		[DataMember(Name = "queuedCount")]
		public int QueuedCount { get; set; }

		/// <summary>
		/// The total count
		/// </summary>
		[DataMember(Name = "totalCount")]
		public int TotalCount { get; set; }

		/// <summary>
		/// The incomplete count
		/// </summary>
		[DataMember(Name = "incompleteCount")]
		public int IncompleteCount { get; set; }

		/// <summary>
		/// The missing count
		/// </summary>
		[DataMember(Name = "missingCount")]
		public int MissingCount { get; set; }
	}
}
