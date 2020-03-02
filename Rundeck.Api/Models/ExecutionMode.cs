using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// ExecutionMode
	/// </summary>
	[DataContract]
	public class ExecutionMode
	{
		/// <summary>
		/// The execution mode
		/// </summary>
		[DataMember(Name = "executionMode")]
		public ExecutionModeEnum ExecutionModeEnum { get; set; } = default;
	}
}
