using Refit;
using Rundeck.Api.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rundeck.Api.Interfaces
{
	public interface IStorage
	{
		/// <summary>
		/// Lists all projects
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/storage/list")]
		Task<List<Plugin>> GetAllAsync(
			CancellationToken cancellationToken = default);
	}
}
