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
			var delay = TimeSpan.FromMilliseconds(250);

			while (true)
			{
				cancellationToken.ThrowIfCancellationRequested();

				PrepareRequest(request);
				await LogAsync("Request", request.ToString(), request.Content, cancellationToken).ConfigureAwait(false);

				var httpResponseMessage = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

				await LogAsync("Response", httpResponseMessage.ToString(), httpResponseMessage.Content, cancellationToken).ConfigureAwait(false);

				if ((int)httpResponseMessage.StatusCode != 429)
				{
					await ThrowForErrorStatusAsync(httpResponseMessage, cancellationToken).ConfigureAwait(false);
					return httpResponseMessage;
				}

				// Back off by increasing amounts with subsequent attempts, with a configurable maximum.
				// There is no maximum total wait time.
				await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
				delay = TimeSpan.FromMilliseconds(Math.Max(delay.TotalMilliseconds * 2, _options.MaxBackOffDelay.TotalMilliseconds));
			}
		}

		private void PrepareRequest(HttpRequestMessage request)
		{
			if (_options.IsReadOnly
				&& request.Method != HttpMethod.Get
				&& request.Method != HttpMethod.Head
				&& request.Method != HttpMethod.Options)
			{
				throw new InvalidOperationException($"The client is read-only; {request.Method} {request.RequestUri?.AbsolutePath} would change the server.");
			}

			if (_options.ApiToken?.Length == 0)
			{
				throw new InvalidOperationException(Resources.TokenIsNotSet);
			}

			request.Headers.Add("X-Rundeck-Auth-Token", _options.ApiToken);
			request.Headers.Add("Accept", "application/json");
		}

		private async Task LogAsync(string title, string summary, HttpContent? content, CancellationToken cancellationToken)
		{
			if (!_logger.IsEnabled(_levelToLogAt))
			{
				return;
			}

			_logger.Log(_levelToLogAt, $"{title}\r\n{summary}");
			if (content != null)
			{
				_logger.Log(_levelToLogAt, $"{title}Content\r\n" + await content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));
			}
		}

		private static async Task ThrowForErrorStatusAsync(HttpResponseMessage response, CancellationToken cancellationToken)
		{
			switch ((int)response.StatusCode)
			{
				case 400:
					throw new RundeckException(await ReadErrorAsync(response, cancellationToken).ConfigureAwait(false));
				case 500:
					throw new RundeckException(response.Content != null
						? await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false)
						: Resources.NoContentBody);
				case 403:
					throw new NotAuthorizedException(await ReadErrorAsync(response, cancellationToken).ConfigureAwait(false));
				default:
					break;
			}
		}

		private static async Task<RundeckError> ReadErrorAsync(HttpResponseMessage response, CancellationToken cancellationToken)
			=> response.Content != null
				? JsonConvert.DeserializeObject<RundeckError>(await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false)) ?? new RundeckError { Message = Resources.NoContentBody }
				: new RundeckError { Message = Resources.NoContentBody };
	}
}
