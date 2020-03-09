using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// The SystemNode
	/// </summary>
	[DataContract]
	public class SystemNode
	{
		/// <summary>
		/// SystemTimestamp
		/// </summary>
		[DataMember(Name = "timestamp")]
		public SystemTimestamp Timestamp { get; set; } = new SystemTimestamp();

		/// <summary>
		/// RundeckInfo
		/// </summary>
		[DataMember(Name = "rundeck")]
		public RundeckInfo Rundeck { get; set; } = new RundeckInfo();

		/// <summary>
		/// Executions
		/// </summary>
		[DataMember(Name = "executions")]
		public Executions Executions { get; set; } = new Executions();

		/// <summary>
		/// OsInfo
		/// </summary>
		[DataMember(Name = "os")]
		public OsInfo OsInfo { get; set; } = new OsInfo();

		/// <summary>
		/// JvmInfo
		/// </summary>
		[DataMember(Name = "jvm")]
		public JvmInfo JvmInfo { get; set; } = new JvmInfo();

		/// <summary>
		/// System Stats
		/// </summary>
		[DataMember(Name = "stats")]
		public Stats Stats { get; set; } = new Stats();

		/// <summary>
		/// Metrics links
		/// </summary>
		[DataMember(Name = "metrics")]
		public Link Metrics { get; set; } = new Link();

		/// <summary>
		/// ThreadDump links
		/// </summary>
		[DataMember(Name = "threadDump")]
		public Link ThreadDump { get; set; } = new Link();

		/// <summary>
		/// Healthcheck links
		/// </summary>
		[DataMember(Name = "healthcheck")]
		public Link Healthcheck { get; set; } = new Link();

		/// <summary>
		/// Ping link
		/// </summary>
		[DataMember(Name = "ping")]
		public Link Ping { get; set; } = new Link();
	}
}