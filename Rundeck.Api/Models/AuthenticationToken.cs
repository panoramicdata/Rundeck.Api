using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// An authentication token
	/// </summary>
	public class AuthenticationToken
	{
		/// <summary>
		/// User
		/// </summary>
		[JsonPropertyName("user")]
		public string User { get; set; } = string.Empty;

		/// <summary>
		/// Id
		/// </summary>
		[JsonPropertyName("id")]
		public string Id { get; set; } = string.Empty;

		/// <summary>
		/// Creator
		/// </summary>
		[JsonPropertyName("creator")]
		public string Creator { get; set; } = string.Empty;

		/// <summary>
		/// Expiration
		/// </summary>
		[JsonPropertyName("expiration")]
		public DateTimeOffset Expiration { get; set; }

		/// <summary>
		/// Roles
		/// </summary>
		[JsonPropertyName("roles")]
		public List<string> Roles { get; set; } = new List<string>();

		/// <summary>
		/// Expired
		/// </summary>
		[JsonPropertyName("expired")]
		public bool Expired { get; set; }

		/// <summary>
		/// Token. Rundeck 3.3 and later return it only in the response that creates the token.
		/// </summary>
		[JsonPropertyName("token")]
		public string Token { get; set; } = string.Empty;
	}
}
