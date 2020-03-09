using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// The Response object when listing ACL Policies
	/// </summary>
	[DataContract]
	public class AclPolicyListing
	{
		/// <summary>
		/// Path
		/// </summary>
		[DataMember(Name = "path")]
		public string Path { get; set; } = default!;

		/// <summary>
		/// Type
		/// Todo: Should be an enum
		/// </summary>
		[DataMember(Name = "type")]
		public string Type { get; set; } = default!;

		/// <summary>
		/// Href
		/// </summary>
		[DataMember(Name = "href")]
		public string Href { get; set; } = default!;

		/// <summary>
		/// The actual list of ACL Policies
		/// </summary>
		[DataMember(Name = "resources")]
		// Todo: This should be an IList<AclPolicy>, but weirdly policyListing does not return a list of "Policy entities"??
		public IList<AclPolicyResource> Policies { get; set; } = new List<AclPolicyResource>();
	}
}
