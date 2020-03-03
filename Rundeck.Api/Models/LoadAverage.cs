using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// LoadAverage
	/// </summary>
	[DataContract]
	public class LoadAverage
	{
		/// <summary>
		/// Unit
		/// </summary>
		[DataMember(Name = "unit")]
		public string Unit { get; set; } = default!;

		/// <summary>
		/// Average
		/// </summary>
		[DataMember(Name = "average")]
		public double Average { get; set; }
	}
}