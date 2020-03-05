using Refit;
using Rundeck.Api.Models;
using Rundeck.Api.Models.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rundeck.Api.Interfaces
{
	public interface IProjects
	{
		/// <summary>
		/// Lists all Projects
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/projects")]
		Task<List<ProjectListingDto>> GetAllAsync(
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a Project Info
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/project/{name}")]
		Task<Project> GetAsync(
			string name,
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

		/// <summary>
		/// Get a Project Config
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/project/{name}/config")]
		Task<Config> GetConfigAsync(
			string name,
			CancellationToken cancellationToken = default);
	}
}
