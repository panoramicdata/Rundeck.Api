using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	public enum JobExecutionStatus
	{
		/// <summary>
		/// Unknown - useful when deserialising
		/// </summary>
		Unknown,

		/// <summary>
		/// Passive
		/// </summary>
		[EnumMember(Value = "running")]
		Running,

		/// <summary>
		/// Succeeded
		/// </summary>
		[EnumMember(Value = "succeeded")]
		Succeeded,

		/// <summary>
		/// Failed
		/// </summary>
		[EnumMember(Value = "failed")]
		Failed,

		/// <summary>
		/// Aborted
		/// </summary>
		[EnumMember(Value = "aborted")]
		Aborted,

		/// <summary>
		/// Timed out
		/// </summary>
		[EnumMember(Value = "timedout")]
		TimedOut,

		/// <summary>
		/// Failed With Retry
		/// </summary>
		[EnumMember(Value = "failed-with-retry")]
		FailedWithRetry,

		/// <summary>
		/// Scheduled
		/// </summary>
		[EnumMember(Value = "scheduled")]
		Scheduled,

		/// <summary>
		/// Other
		/// </summary>
		[EnumMember(Value = "other")]
		Other,
	}
}
