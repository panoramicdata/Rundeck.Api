using Refit;
using System.Threading;
using System.Threading.Tasks;

namespace Rundeck.Api.Interfaces
{
	/// <summary>
	/// Executions interface
	/// </summary>
	public interface IExecutions
	{
		/// <summary>
		/// Deletes an execution
		/// </summary>
		/// <param name="id">The execution id</param>
		/// <param name="cancellationToken"></param>
		[Delete("/execution/{id}")]
		Task DeleteAsync(
			int id,
			CancellationToken cancellationToken = default);
	}
}
