using Refit;
using Rundeck.Api.Models;
using System.Collections.Generic;
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
		/// Get Execution Info
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[Get("/execution/{id}")]
		Task<Execution> GetAsync(
			int id,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// List Input Files for an Execution
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[Get("/execution/{id}/input/files")]
		Task<JobFileListingResult> GetFilesAsync(
			int id,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Deletes an execution
		/// </summary>
		/// <param name="id">The execution id</param>
		/// <param name="cancellationToken"></param>
		[Delete("/execution/{id}")]
		Task DeleteAsync(
			int id,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Bulk delete Executions
		/// </summary>
		/// <param name="idlist"></param>
		/// <param name="cancellationToken"></param>
		[Post("/executions/delete")]
		[Headers("application/json")]
		Task DeleteAsync(
			[Body] Dictionary<string, List<int>> idlist,
			CancellationToken cancellationToken = default);

	}
}
