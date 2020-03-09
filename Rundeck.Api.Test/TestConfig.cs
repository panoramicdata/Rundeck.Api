using System;

namespace Rundeck.Api.Test
{
	/// <summary>
	/// XUnit test configuration
	/// </summary>
	public class TestConfig
	{
		/// <summary>
		/// The server Uri
		/// </summary>
		public Uri? Uri { get; set; } = default;

		/// <summary>
		/// The test username
		/// </summary>
		public string Username { get; set; } = string.Empty;

		/// <summary>
		/// The token to use for authentication
		/// </summary>
		public string Token { get; set; } = string.Empty;
	}
}
