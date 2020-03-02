using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Rundeck.Api.Exceptions;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Rundeck.Api
{
	internal class AuthenticatedBackingOffHttpClientHandler : HttpClientHandler
	{
		private readonly RundeckClientOptions _options;
		private readonly ILogger _logger;
		private readonly LogLevel _levelToLogAt = LogLevel.Trace;

		public AuthenticatedBackingOffHttpClientHandler(RundeckClientOptions options)
		{
			_options = options;
			_logger = options.Logger ?? NullLogger.Instance;
		}

		protected override async Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage request,
			CancellationToken cancellationToken)
		{
			HttpResponseMessage httpResponseMessage;
			var delay = TimeSpan.FromMilliseconds(250);

			while (true)
			{
				cancellationToken.ThrowIfCancellationRequested();

				// Ensure the API token is set
				if (_options.ApiToken?.Length == 0)
				{
					throw new InvalidOperationException(Resources.TokenIsNotSet);
				}
				// The API Key is set

				// Add the request headers
				request.Headers.Add("X-Rundeck-Auth-Token", _options.ApiToken);
				request.Headers.Add("Accept", "application/json");

				// Only do any of this if we're at the level we want to enable for
				if (_logger.IsEnabled(_levelToLogAt))
				{
					_logger.Log(_levelToLogAt, $"Request\r\n{request}");
					if (request.Content != null)
					{
						_logger.Log(_levelToLogAt, "RequestContent\r\n" + await request.Content.ReadAsStringAsync().ConfigureAwait(false));
					}
				}

				// Complete the action
				httpResponseMessage = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

				// Log our response before we do anything else
				if (_logger.IsEnabled(_levelToLogAt))
				{
					_logger.Log(_levelToLogAt, $"Response\r\n{httpResponseMessage}");
					if (httpResponseMessage.Content != null)
					{
						_logger.Log(_levelToLogAt, "ResponseContent\r\n" + await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false));
					}
				}

				switch ((int)httpResponseMessage.StatusCode)
				{
					case 400:
						throw new RundeckException(httpResponseMessage.Content != null
							? JsonConvert.DeserializeObject<RundeckError>(await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false))
							: new RundeckError { Message = Resources.NoContentBody });
					case 500:
						throw new RundeckException(httpResponseMessage.Content != null
							? await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false)
							: Resources.NoContentBody);
					case 429:
						// We have a 429.  Back off by increasing amounts with subsequent attempts, with a configurable maximum.
						// There is no maximum total wait time.
						await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
						delay = TimeSpan.FromMilliseconds(Math.Max(delay.TotalMilliseconds * 2, _options.MaxBackOffDelay.TotalMilliseconds));
						continue;
					default:
						return httpResponseMessage;
				}
			}
		}
	}
}
