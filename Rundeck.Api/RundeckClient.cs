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
			_httpClient = new HttpClient(_httpClientHandler) { BaseAddress = new Uri(options.Uri, $"/api/{options.ApiVersion}") };

			var refitSettings = new RefitSettings
			{
				ContentSerializer = new JsonContentSerializer(
					new JsonSerializerSettings
					{
						NullValueHandling = NullValueHandling.Ignore
#if DEBUG
						,
						MissingMemberHandling = MissingMemberHandling.Error
#endif
					}
				)
			};

			AuthenticationTokens = RestService.For<IAuthenticationTokens>(_httpClient, refitSettings);
			Executions = RestService.For<IExecutions>(_httpClient, refitSettings);
			Metrics = RestService.For<IMetrics>(_httpClient, refitSettings);
			Plugins = RestService.For<IPlugins>(_httpClient, refitSettings);
			Projects = RestService.For<IProjects>(_httpClient, refitSettings);
			Storage = RestService.For<IStorage>(_httpClient, refitSettings);
			System = RestService.For<ISystem>(_httpClient, refitSettings);
			Users = RestService.For<IUsers>(_httpClient, refitSettings);
			WebHooks = RestService.For<IWebHooks>(_httpClient, refitSettings);
		}

		/// <summary>
		/// Authentication tokens
		/// </summary>
		public IAuthenticationTokens AuthenticationTokens { get; }

		/// <summary>
		/// Executions
		/// </summary>
		public IExecutions Executions { get; }

		/// <summary>
		/// Metrics
		/// </summary>
		public IMetrics Metrics { get; }

		/// <summary>
		/// Plugins
		/// </summary>
		public IPlugins Plugins { get; }

		/// <summary>
		/// Projects
		/// </summary>
		public IProjects Projects { get; }

		/// <summary>
		/// Storage
		/// </summary>
		public IStorage Storage { get; }

		/// <summary>
		/// System
		/// </summary>
		public ISystem System { get; }

		/// <summary>
		/// Users
		/// </summary>
		public IUsers Users { get; }

		/// <summary>
		/// Users
		/// </summary>
		public IWebHooks WebHooks { get; }

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
