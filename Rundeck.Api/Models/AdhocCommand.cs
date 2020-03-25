using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class AdhocCommand
	{
		/// <summary>
		/// Name of the Project
		/// </summary>
		[DataMember(Name = "project")]
		public string Project { get; set; } = default!;

		/// <summary>
		/// The shell command string to run
		/// </summary>
		[DataMember(Name = "exec")]
		public string Exec { get; set; } = default!;

		/// <summary>
		/// Threadcount to use
		/// </summary>
		[DataMember(Name = "nodeThreadcount")]
		public int? NodeThreadcount { get; set; }

		/// <summary>
		/// Continue executing on other nodes even if some fail
		/// </summary>
		[DataMember(Name = "nodeKeepgoing")]
		public bool? NodeKeepgoing { get; set; }

		/// <summary>
		/// The User who ran the command
		/// </summary>
		[DataMember(Name = "asUser")]
		public string? AsUser { get; set; } = default!;

		[DataMember(Name = "filter")]
		public string? Filter { get; set; } = default!;
	}
}