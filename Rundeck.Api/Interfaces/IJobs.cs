using Refit;
using Rundeck.Api.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rundeck.Api.Interfaces
{
	public interface IJobs
	{
		/// <summary>
		/// Lists all job
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/job/list")]
		Task<List<Plugin>> GetAllAsync(
			CancellationToken cancellationToken = default);
	}
}
