using Refit;
using Rundeck.Api.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Rundeck.Api.Interfaces
{
	public interface IMetrics
	{
		/// <summary>
		/// Gets a list of tokens
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/metrics")]
		Task<Metrics> GetAsync(
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets the metrics metrics
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/metrics/metrics")]
		Task<MetricsMetrics> GetMetricsAsync(
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets the healthcheck metrics
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/metrics/healthcheck")]
		Task<HealthCheckMetrics> GetHealthCheckAsync(
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Ping the metrics endpoint
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/metrics/ping")]
		Task<string> PingAsync(
			CancellationToken cancellationToken = default);
	}
}
