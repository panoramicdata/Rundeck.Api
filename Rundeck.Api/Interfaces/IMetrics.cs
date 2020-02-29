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
		[Get("/api/25/metrics")]
		Task<Metrics> GetAsync(
			CancellationToken cancellationToken = default);
	}
}
