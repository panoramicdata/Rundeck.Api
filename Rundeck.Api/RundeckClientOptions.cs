using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Rundeck.Api.Exceptions;
using System;

namespace Rundeck.Api
{
	/// <summary>
	/// The Rundeck client options
	/// </summary>
	public class RundeckClientOptions
	{
		// This is the version of the API we support
		public static readonly int SupportedApiVersion = 34;

		/// <summary>
		/// The API token
		/// </summary>
		public string? ApiToken { get; set; }

		/// <summary>
		///  The maximum back-off delay..
		///  Back-offs double in duration until this point,
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
		/// The Rundeck API Version
		/// </summary>
		public int ApiVersion { get; } = SupportedApiVersion;

		/// <summary>
		/// An optional logger
		/// </summary>
		public ILogger Logger { get; set; } = new NullLogger<RundeckClientOptions>();

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

			if (MaxBackOffDelay < TimeSpan.Zero)
			{
				throw new ConfigurationException(Resources.NegativeMaxBackoffDelay);
			}
		}
	}
}