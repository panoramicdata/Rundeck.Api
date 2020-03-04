using Refit;
using Rundeck.Api.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rundeck.Api.Interfaces
{
	public interface IProjects
	{
		/// <summary>
		/// Lists all projects
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/projects")]
		Task<List<Project>> GetAllAsync(
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Create a Project
		/// </summary>
		[Post("/projects")]
		Task<Project> CreateAsync(
			[Body] Project project,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Delete a Project
		/// </summary>
		[Delete("/project/{name}")]
		Task<Project> DeleteAsync(
			string name,
			CancellationToken cancellationToken = default);
	}
}
