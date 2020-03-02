﻿using Refit;
using System.Threading;
using System.Threading.Tasks;

namespace Rundeck.Api.Interfaces
{
	public interface IExecutions
	{
		/// <summary>
		/// Deletes an execution
		/// </summary>
		/// <param name="id">The execution id</param>
		/// <param name="cancellationToken"></param>
		[Delete("/api/32/execution/{id}")]
		Task DeleteAsync(
			int id,
			CancellationToken cancellationToken = default);
	}
}