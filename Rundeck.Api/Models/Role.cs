using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// A set of roles
	/// </summary>
	public class RoleSet
	{
		/// <summary>
		/// The roles
		/// </summary>
		[JsonPropertyName("roles")]
		public List<string> Roles { get; set; } = new List<string>();
	}
}
