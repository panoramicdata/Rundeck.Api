using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// An ACL Policy
	/// </summary>
	[DataContract]
	public class AclPolicy
	{
		/// <summary>
		/// The contents of the policy
		/// </summary>
		[DataMember(Name = "contents")]
		public string Contents { get; set; } = default!;
	}
}