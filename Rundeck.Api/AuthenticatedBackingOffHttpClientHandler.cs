using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Rundeck.Api
{
	internal class AuthenticatedBackingOffHttpClientHandler : HttpClientHandler
	{
		private readonly RundeckClientOptions _options;

		public AuthenticatedBackingOffHttpClientHandler(RundeckClientOptions options)
		{
			_options = options;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
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

				// Complete the action
				httpResponseMessage = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

				if ((int)httpResponseMessage.StatusCode != 429)
				{
					return httpResponseMessage;
				}

				// We havea 429.  Back off by increasing amounts with subsequent attempts, with a configurable maximum.
				// There is no maximum total wait time.
				await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
				delay = TimeSpan.FromMilliseconds(Math.Max(delay.TotalMilliseconds * 2, _options.MaxBackOffDelay.TotalMilliseconds));
			}
		}
	}
}
