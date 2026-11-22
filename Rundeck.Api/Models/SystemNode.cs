using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// The SystemNode
	/// </summary>
	public class SystemNode
	{
		/// <summary>
		/// SystemTimestamp
		/// </summary>
		[JsonPropertyName("timestamp")]
		public SystemTimestamp Timestamp { get; set; } = new SystemTimestamp();

		/// <summary>
		/// RundeckInfo
		/// </summary>
		[JsonPropertyName("rundeck")]
		public RundeckInfo Rundeck { get; set; } = new RundeckInfo();

		/// <summary>
		/// Executions
		/// </summary>
		[JsonPropertyName("executions")]
		public Executions Executions { get; set; } = new Executions();

		/// <summary>
		/// OsInfo
		/// </summary>
		[JsonPropertyName("os")]
		public OsInfo OsInfo { get; set; } = new OsInfo();

		/// <summary>
		/// JvmInfo
		/// </summary>
		[JsonPropertyName("jvm")]
		public JvmInfo JvmInfo { get; set; } = new JvmInfo();

		/// <summary>
		/// System Stats
		/// </summary>
		[JsonPropertyName("stats")]
		public Stats Stats { get; set; } = new Stats();

		/// <summary>
		/// Metrics links
		/// </summary>
		[JsonPropertyName("metrics")]
		public Link Metrics { get; set; } = new Link();

		/// <summary>
		/// ThreadDump links
		/// </summary>
		[JsonPropertyName("threadDump")]
		public Link ThreadDump { get; set; } = new Link();

		/// <summary>
		/// Healthcheck links
		/// </summary>
		[JsonPropertyName("healthcheck")]
		public Link Healthcheck { get; set; } = new Link();

		/// <summary>
		/// Extended system information, when the server provides it
		/// </summary>
		[JsonPropertyName("extended")]
		public object? Extended { get; set; }

		/// <summary>
		/// Ping link
		/// </summary>
		[JsonPropertyName("ping")]
		public Link Ping { get; set; } = new Link();
	}
}