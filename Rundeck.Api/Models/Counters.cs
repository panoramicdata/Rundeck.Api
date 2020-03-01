using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{

	/// <summary>
	/// Counters
	/// </summary>
	[DataContract]
	public class Counters
	{
		/// <summary>
		/// rundeckschedulerquartzscheduledJobs
		/// </summary>
		[DataMember(Name = "rundeckschedulerquartzscheduledJobs")]
		public Counter rundeckschedulerquartzscheduledJobs { get; set; }
	}
}
