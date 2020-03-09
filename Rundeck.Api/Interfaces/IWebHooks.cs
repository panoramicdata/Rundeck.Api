using Refit;
using Rundeck.Api.Models;
using Rundeck.Api.Models.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rundeck.Api.Interfaces
{
	public interface IWebHooks
	{
		/// <summary>
		/// Lists all web hooks for a project
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/project/{projectName}/webhooks")]
		Task<List<WebHook>> GetAllAsync(
			string projectName,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Create a web hook for a project
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Post("/project/{projectName}/webhook")]
		Task<string> CreateAsync(
			string projectName,
			[Body] WebHookCreationDto webHook,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Update a web hook
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Post("/project/{projectName}/webhook/{id}")]
		Task<string> UpdateAsync(
			string projectName,
			int id,
			[Body] WebHook webHook,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Delete a web hook
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Delete("/project/{projectName}/webhook/{id}")]
		Task<string> DeleteAsync(
			string projectName,
			int id,
			CancellationToken cancellationToken = default);
	}
}
