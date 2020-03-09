using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// An AclPolicy when listing policies
	/// </summary>
	[DataContract]
	public class AclPolicyResource
	{
		/// <summary>
		/// Path
		/// </summary>
		[DataMember(Name = "path")]
		public string Path { get; set; } = default!;

		/// <summary>
		/// Name
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; } = default!;

		/// <summary>
		/// Type
		/// Todo: Should be an enum, gather possible values
		/// </summary>
		[DataMember(Name = "type")]
		public string Type { get; set; } = default!;

		/// <summary>
		/// Href
		/// </summary>
		[DataMember(Name = "href")]
		public string Href { get; set; } = default!;
	}
}