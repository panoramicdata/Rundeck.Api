using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Operating System Info
	/// </summary>
	[DataContract]
	public class OsInfo
	{
		/// <summary>
		/// Arch
		/// </summary>
		[DataMember(Name = "arch")]
		public string Arch { get; set; } = default!;

		/// <summary>
		/// Name
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; } = default!;

		/// <summary>
		/// Version
		/// </summary>
		[DataMember(Name = "version")]
		public string Version { get; set; } = default!;
	}
}