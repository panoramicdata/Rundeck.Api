using Refit;
using Rundeck.Api.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Rundeck.Api.Interfaces
{
	public interface ISystem
	{
		/// <summary>
		/// Gets the Log Storage
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/api/17/system/logstorage")]
		Task<LogStorage> GetLogStorageAsync(
			CancellationToken cancellationToken = default);
	}
}
