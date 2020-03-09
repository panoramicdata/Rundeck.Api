using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// A set of roles
	/// </summary>
	[DataContract]
	public class RoleSet
	{
		/// <summary>
		/// The roles
		/// </summary>
		[DataMember(Name = "roles")]
		public List<string> Roles { get; set; } = new List<string>();
	}
}
