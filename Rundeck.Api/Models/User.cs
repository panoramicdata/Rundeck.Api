using System;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// A user
	/// </summary>
	public class User
	{
		/// <summary>
		/// The login
		/// </summary>
		[JsonPropertyName("login")]
		public string Login { get; set; } = string.Empty;

		/// <summary>
		/// First name
		/// </summary>
		[JsonPropertyName("firstName")]
		public string FirstName { get; set; } = string.Empty;

		/// <summary>
		/// Last name
		/// </summary>
		[JsonPropertyName("lastName")]
		public string LastName { get; set; } = string.Empty;

		/// <summary>
		/// Email address
		/// </summary>
		[JsonPropertyName("email")]
		public string Email { get; set; } = string.Empty;

		/// <summary>
		/// Creation time
		/// </summary>
		[JsonPropertyName("created")]
		public DateTimeOffset Created { get; set; } = DateTimeOffset.MinValue;

		/// <summary>
		/// Last update time
		/// </summary>
		[JsonPropertyName("updated")]
		public DateTimeOffset? Updated { get; set; }

		/// <summary>
		/// Time of the user's last job execution, if any
		/// </summary>
		[JsonPropertyName("lastJob")]
		public DateTimeOffset? LastJob { get; set; }

		/// <summary>
		/// The number of tokens
		/// </summary>
		[JsonPropertyName("tokens")]
		public int TokenCount { get; set; }
	}
}
