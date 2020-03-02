using Refit;
using Rundeck.Api.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rundeck.Api.Interfaces
{
	public interface IWebHooks
	{
		/// <summary>
		/// Lists all web  hooks
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/api/33/webhook/list")]
		Task<List<Plugin>> GetAllAsync(
			CancellationToken cancellationToken = default);
	}
}
