using Refit;
using Rundeck.Api.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Rundeck.Api.Interfaces
{
	public interface IPolicies
	{
		/// <summary>
		/// Gets a list of ACL Policies
		/// </summary>
		/// <param name="cancellationToken"></param>
		[Get("/system/acl/")]
		Task<AclPolicyListing> GetAllAsync(
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Gets a specific AclPolicy
		/// </summary>
		/// <param name="id">The token id</param>
		[Get("/system/acl/{id}")]
		Task<AclPolicy> GetAsync(
			string id,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Create an ACL Policy
		/// </summary>
		[Post("/system/acl/{name}")]
		Task<AclPolicy> CreateAsync(
			string name,
			[Body] AclPolicy aclPolicy,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Update an ACL Policy
		/// </summary>
		[Put("/system/acl/{name}")]
		Task<AclPolicy> UpdateAsync(
			string name,
			[Body] AclPolicy aclPolicy,
			CancellationToken cancellationToken = default);

		/// <summary>
		/// Delete an AclPolicy
		/// </summary>
		[Delete("/system/acl/{name}")]
		Task DeleteAsync(
			string name,
			CancellationToken cancellationToken = default);
	}
}