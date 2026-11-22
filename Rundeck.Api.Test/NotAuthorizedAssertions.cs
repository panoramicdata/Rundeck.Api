using AwesomeAssertions;
using Rundeck.Api.Exceptions;
using System;
using System.Threading.Tasks;

namespace Rundeck.Api.Test
{
	internal static class NotAuthorizedAssertions
	{
		/// <summary>Refit wraps what the HTTP handler throws, so a 403 reaches callers as the inner exception.</summary>
		internal static async Task ShouldThrowNotAuthorizedAsync(Func<Task> act)
		{
			var thrown = (await act.Should().ThrowAsync<Exception>()).Which;

			thrown.InnerException.Should().BeOfType<NotAuthorizedException>();
		}
	}
}