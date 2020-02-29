using Rundeck.Api.Exceptions;
using System;

namespace Rundeck.Api
{
	/// <summary>
	/// The Rundeck client options
	/// </summary>
	public class RundeckClientOptions
	{
		/// <summary>
		/// The API token
		/// </summary>
		public string? ApiToken { get; set; }

		/// <summary>
		///  The maximum back-off delay..
		///  Backoffs double in duration until this point,
		///  after which the delay is maintained at this
		///  level until the back-off instructions are no
		///  longer sent.
		/// </summary>
		public TimeSpan MaxBackOffDelay { get; set; } = TimeSpan.FromSeconds(30);

		/// <summary>
		/// The Uri
		/// </summary>
		public Uri? Uri { get; set; }

		/// <summary>
		/// Validates the options provided are valid.  If not, a ConfigruationException is thrown.
		/// </summary>
		public void Validate()
		{
			if (Uri == null)
			{
				throw new ConfigurationException(Resources.UriIsNotSet);
			}

			if (string.IsNullOrWhiteSpace(ApiToken))
			{
				throw new ConfigurationException(Resources.TokenIsNotSet);
			}
		}
	}
}