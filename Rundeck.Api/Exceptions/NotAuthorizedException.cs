using System;

namespace Rundeck.Api.Exceptions
{
	public class NotAuthorizedException : RundeckException
	{
		public NotAuthorizedException()
		{
		}

		public NotAuthorizedException(RundeckError rundeckError) : base(rundeckError)
		{
		}

		public NotAuthorizedException(string message) : base(message)
		{
		}

		public NotAuthorizedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
