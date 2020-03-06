using Refit;
using Rundeck.Api.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Rundeck.Api.Interfaces
{
	public interface IKeys
	{
		/// <summary>
		/// Get keys
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/storage/keys/{path}")]
		Task<KeyStorage> GetAsync(
			string path,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Create a private key
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Post("/storage/keys/{path}")]
		[Headers("Content-Type: application/octet-stream")]
		Task<KeyStorage> CreatePrivateKeyAsync(
			string path,
			[Body]string privateKey,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Create a private key
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Post("/storage/keys/{path}")]
		[Headers("Content-Type: application/pgp-keys")]
		Task<KeyStorage> CreatePublicKeyAsync(
			string path,
			[Body]string publicKey,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Create a private key
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Post("/storage/keys/{path}")]
		[Headers("Content-Type: x-rundeck-data-password")]
		Task<KeyStorage> CreatePasswordAsync(
			string path,
			[Body]string password,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Delete("/storage/keys/{path}")]
		Task<KeyStorage> DeleteAsync(
			string path,
			CancellationToken cancellationToken = default);
	}
}
