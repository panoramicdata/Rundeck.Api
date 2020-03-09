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
		/// Lists all job for a project
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/project/{projectName}/jobs")]
		Task<List<Job>> GetAllAsync(
			string projectName,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Get job definition
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// Todo - Implement FileDownload to download JobDefinition in XML/YAML format
		[Get("/job/{id}")]
		[Headers("Content-Type: application/yaml")]
		Task<string> GetAsync(
			string id,
			[AliasAs("format")] JobFileFormat fileFormat,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Import job for a project from a definition
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Post("/project/{projectName}/jobs/import")]
		[Headers("Content-Type: application/yaml")]
		Task<JobImportResults> ImportAsync(
			string projectName,
			[Body] string jobContent,
			[AliasAs("fileformat")] JobFileFormat fileFormat,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Delete a job
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Delete("/job/{id}")]
		Task<JobImportResults> DeleteAsync(
			string id,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Enable Executions on a Job
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[Post("/job/{id}/execution/enable")]
		Task<SuccessResult> EnableExecutionsAsync(
			string id,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Disable Executions on a Job
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[Post("/job/{id}/execution/disable")]
		Task<SuccessResult> DisableExecutionsAsync(
			string id,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Enable Scheduling on a Job
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[Post("/job/{id}/schedule/enable")]
		Task<SuccessResult> EnableSchedulingAsync(
			string id,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Disable Scheduling on a Job
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[Post("/job/{id}/schedule/disable")]
		Task<SuccessResult> DisableSchedulingAsync(
			string id,
			CancellationToken cancellationToken = default);
	}
}
