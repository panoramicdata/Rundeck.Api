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
		[Get("/system/logstorage")]
		Task<LogStorage> GetLogStorageAsync(
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets the incomplete Log Storage
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/system/logstorage/incomplete")]
		Task<IncompleteLogStorage> GetIncompleteLogStorageAsync(
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Resume processing incomplete Log Storage uploads.
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Post("/system/logstorage/incomplete/resume")]
		Task<ResumeIncompleteLogStorageResult> ResumeIncompleteLogStorageAsync(
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Enables executions, allowing adhoc and manual and scheduled jobs to be run.
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Post("/system/executions/enable")]
		Task<ExecutionMode> SetActiveModeAsync(
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Disables executions, allowing adhoc and manual and scheduled jobs to be run.
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Post("/system/executions/disable")]
		Task<ExecutionMode> SetPassiveModeAsync(
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets the current execution mode. Additionally, if the current mode is passive the response status will be HTTP 503 - Service Unavailable.
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/system/executions/status")]
		Task<ExecutionMode> GetExecutionModeAsync(
			CancellationToken cancellationToken = default);
	}
}
