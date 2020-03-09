using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// A CPU info
	/// </summary>
	[DataContract]
	public class CpuInfo
	{
		/// <summary>
		/// The loadAverage
		/// </summary>
		[DataMember(Name = "loadAverage")]
		public LoadAverage LoadAverage { get; set; } = new LoadAverage();

		/// <summary>
		/// The number of processors
		/// </summary>
		[DataMember(Name = "processors")]
		public int Processors { get; set; }
	}
}