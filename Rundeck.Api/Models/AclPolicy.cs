using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// An ACL Policy
	/// </summary>
	public class AclPolicy
	{
		/// <summary>
		/// The contents of the policy
		/// </summary>
		[JsonPropertyName("contents")]
		public string Contents { get; set; } = default!;
	}
}