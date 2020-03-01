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
		public string Uri { get; set; } = string.Empty;

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
