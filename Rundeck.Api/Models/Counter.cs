using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Counter
	/// </summary>
	[DataContract]
	public class Counter
	{
		/// <summary>
		/// The count
		/// </summary>
		[DataMember(Name = "count")]
		public int Count { get; set; }
	}
}
