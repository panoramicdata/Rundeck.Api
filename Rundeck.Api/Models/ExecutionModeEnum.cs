using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Execution mode enum
	/// </summary>
	public enum ExecutionModeEnum
	{
		/// <summary>
		/// Unknown - useful when deserialising
		/// </summary>
		Unknown,

		/// <summary>
		/// Passive
		/// </summary>
		[EnumMember(Value = "passive")]
		Passive,

		/// <summary>
		/// Active
		/// </summary>
		[EnumMember(Value = "active")]
		Active
	}
}