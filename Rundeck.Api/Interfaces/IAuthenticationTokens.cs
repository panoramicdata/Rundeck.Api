using Refit;
using Rundeck.Api.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rundeck.Api.Interfaces
{
	public interface IAuthenticationTokens
	{
		/// <summary>
		/// Gets a list of tokens
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/api/11/tokens")]
		Task<List<AuthenticationToken>> GetAllAsync(
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets a list of tokens for a specific user
		/// </summary>
		/// <param name="username">The username</param>
		[Get("/api/11/tokens/{username}")]
		Task<List<AuthenticationToken>> GetAllByUserAsync(
			string username,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets a specific token
		/// </summary>
		/// <param name="id">The token id</param>
		[Get("/api/11/token/{id}")]
		Task<AuthenticationToken> GetAsync(
			string id,
			CancellationToken cancellationToken = default);
	}
}
