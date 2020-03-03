using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// JvmInfo
	/// </summary>
	[DataContract]
	public class JvmInfo
	{
		/// <summary>
		/// Name
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; } = default!;

		/// <summary>
		/// Vendor
		/// </summary>
		[DataMember(Name = "vendor")]
		public string Vendor { get; set; } = default!;

		/// <summary>
		/// Version
		/// </summary>
		[DataMember(Name = "version")]
		public string Version { get; set; } = default!;

		/// <summary>
		/// Implementation version
		/// </summary>
		[DataMember(Name = "implementationVersion")]
		public string ImplementationVersion { get; set; } = default!;
	}
}