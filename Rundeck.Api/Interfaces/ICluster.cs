using Refit;
using Rundeck.Api.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rundeck.Api.Interfaces
{
	public interface ICluster
	{
		/// <summary>
		/// Gets a list of scheduled jobs for this cluster server
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/scheduler/jobs/")]
		Task<List<Job>> GetAllJobsAsync(
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets a list of scheduled jobs for another cluster server
		/// </summary>
		/// <param name="uuid"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[Get("/scheduler/server/{uuid}/jobs")]
		Task<List<Job>> GetAllJobsAsync(
			string uuid,
			CancellationToken cancellationToken = default);
	}
}