using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Executions
	/// </summary>
	[DataContract]
	public class Executions
	{
		/// <summary>
		/// Execution status
		/// </summary>
		[DataMember(Name = "active")]
		public bool Active { get; set; }

		/// <summary>
		/// ExecutionMode
		/// </summary>
		[DataMember(Name = "executionMode")]
		public ExecutionModeEnum ExecutionMode { get; set; }
	}
}