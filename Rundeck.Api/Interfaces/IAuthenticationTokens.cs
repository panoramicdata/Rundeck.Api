using Refit;
using Rundeck.Api.Models;
using Rundeck.Api.Models.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rundeck.Api.Interfaces
{
	/// <summary>
	/// AuthenticationTokens interface
	/// </summary>
	public interface IAuthenticationTokens
	{
		/// <summary>
		/// Gets a list of tokens
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/tokens")]
		Task<List<AuthenticationToken>> GetAllAsync(
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets a list of tokens for a specific user
		/// </summary>
		/// <param name="username">The username</param>
		[Get("/tokens/{username}")]
		Task<List<AuthenticationToken>> GetAllByUserAsync(
			string username,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets a specific token
		/// </summary>
		/// <param name="id">The token id</param>
		[Get("/token/{id}")]
		Task<AuthenticationToken> GetAsync(
			string id,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Create a token
		/// </summary>
		[Post("/tokens")]
		Task<AuthenticationToken> CreateAsync(
			[Body] AuthenticationTokenCreationDto authenticationTokenCreationDto,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Delete a token
		/// </summary>
		[Delete("/token/{id}")]
		Task<AuthenticationToken> DeleteAsync(
			string id,
			CancellationToken cancellationToken = default);
	}
}
