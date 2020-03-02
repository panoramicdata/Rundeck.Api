using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Incomplete log storage
	/// </summary>
	[DataContract]
	public class IncompleteLogStorage
	{
		/// <summary>
		/// The total
		/// </summary>
		[DataMember(Name = "total")]
		public int Total { get; set; }

		/// <summary>
		/// The max
		/// </summary>
		[DataMember(Name = "max")]
		public int Max { get; set; }

		/// <summary>
		/// The offset
		/// </summary>
		[DataMember(Name = "offset")]
		public int Offset { get; set; }

		/// <summary>
		/// The executions
		/// </summary>
		[DataMember(Name = "executions")]
		public List<Execution> Executions { get; set; } = new List<Execution>();
	}
}
