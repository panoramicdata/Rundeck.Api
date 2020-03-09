using Refit;
using Rundeck.Api.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rundeck.Api.Interfaces
{
	public interface IPlugins
	{
		/// <summary>
		/// Lists all plugins
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Delete("/plugin/list")]
		Task<List<Plugin>> GetAllAsync(
			CancellationToken cancellationToken = default);
	}
}
