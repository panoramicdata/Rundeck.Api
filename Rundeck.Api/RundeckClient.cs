using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Refit;
using Rundeck.Api.Exceptions;
using Rundeck.Api.Interfaces;
using System;
using System.Net.Http;

namespace Rundeck.Api
{
	public class RundeckClient : IDisposable
	{
		private readonly ILogger _logger;
		private readonly HttpClient _httpClient;
		private readonly AuthenticatedBackingOffHttpClientHandler _httpClientHandler;

		public RundeckClient(RundeckClientOptions options, ILogger logger = default!)
		{
			if (options == null)
			{
				throw new ConfigurationException(Resources.OptionsAreMissing);
			}
			options.Validate();
			_logger = logger ?? NullLogger.Instance;
			_httpClientHandler = new AuthenticatedBackingOffHttpClientHandler(options ?? throw new ArgumentNullException(nameof(options)));
			_httpClient = new HttpClient(_httpClientHandler) { BaseAddress = options.Uri };

			var refitSettings = new RefitSettings
			{
				ContentSerializer = new JsonContentSerializer(
				new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore
				})
			};

			AuthenticationTokens = RestService.For<IAuthenticationTokens>(_httpClient, refitSettings);
			Metrics = RestService.For<IMetrics>(_httpClient, refitSettings);
		}

		public IAuthenticationTokens AuthenticationTokens { get; }

		public IMetrics Metrics { get; }

		#region IDisposable Support
		private bool _disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					_logger.LogDebug(Resources.Disposing);
					_httpClient.Dispose();
					_httpClientHandler.Dispose();
					_logger.LogDebug(Resources.Disposed);
				}

				_disposedValue = true;
			}
		}

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);

			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
