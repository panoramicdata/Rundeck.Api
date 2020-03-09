using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// MemoryInfo
	/// </summary>
	[DataContract]
	public class MemoryInfo
	{
		/// <summary>
		/// Unit
		/// </summary>
		[DataMember(Name = "unit")]
		public string Unit { get; set; } = default!;

		/// <summary>
		/// Max value
		/// </summary>
		[DataMember(Name = "max")]
		public long Max { get; set; }

		/// <summary>
		/// Free
		/// </summary>
		[DataMember(Name = "free")]
		public long Free { get; set; }

		/// <summary>
		/// Total
		/// </summary>
		[DataMember(Name = "total")]
		public long Total { get; set; }
	}
}