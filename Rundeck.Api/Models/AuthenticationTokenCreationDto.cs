using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// An authentication token creation DTO
	/// </summary>
	[DataContract]
	public class AuthenticationTokenCreationDto
	{
		/// <summary>
		/// User
		/// </summary>
		[DataMember(Name = "user")]
		public string User { get; set; } = string.Empty;

		/// <summary>
		/// Duration, e.g. 30d for 30 days
		/// </summary>
		[DataMember(Name = "duration")]
		public string Duration { get; set; } = "30d";

		/// <summary>
		/// The list of roles the token should use.
		/// Leave as null for all user roles
		/// </summary>
		[DataMember(Name = "roles")]
		public List<string>? Roles { get; set; }
	}
}
