using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models.Dtos
{
	/// <summary>
	/// An authentication token creation DTO
	/// </summary>
	public class AuthenticationTokenCreationDto
	{
		/// <summary>
		/// User
		/// </summary>
		[JsonPropertyName("user")]
		public string User { get; set; } = string.Empty;

		/// <summary>
		/// Duration, e.g. 30d for 30 days
		/// </summary>
		[JsonPropertyName("duration")]
		public string Duration { get; set; } = "30d";

		/// <summary>
		/// The list of roles the token should use.
		/// Leave as null for all user roles
		/// </summary>
		[JsonPropertyName("roles")]
		public List<string>? Roles { get; set; }
	}
}
