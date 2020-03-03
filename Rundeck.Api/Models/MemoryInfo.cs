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
		public int Max { get; set; }

		/// <summary>
		/// Free
		/// </summary>
		[DataMember(Name = "free")]
		public int Free { get; set; }

		/// <summary>
		/// Total
		/// </summary>
		[DataMember(Name = "total")]
		public int Total { get; set; }
	}
}