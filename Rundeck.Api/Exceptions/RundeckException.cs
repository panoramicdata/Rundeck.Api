using System;

namespace Rundeck.Api.Exceptions
{
	public class RundeckException : Exception
	{
		public RundeckError? RundeckError { get; }

		public RundeckException()
		{
		}

		public RundeckException(RundeckError rundeckError)
			: base(rundeckError?.Message ?? throw new ArgumentNullException(nameof(rundeckError)))
			=> RundeckError = rundeckError;

		public RundeckException(string message) : base(message)
		{
		}

		public RundeckException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
