using AwesomeAssertions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Rundeck.Api.Test
{
	/// <summary>The API token is sent as a header but must never reach the logs.</summary>
	public class TokenRedactionTests
	{
		private const string Token = "redaction-test-token-value";

		[Fact]
		public async Task TraceLogging_DoesNotContainTheApiToken()
		{
			using var server = new StubRundeckServer("[]");
			var logger = new CapturingLogger();
			using var client = new RundeckClient(
				new RundeckClientOptions { Uri = server.Uri, ApiToken = Token, Logger = logger });

			await client.Jobs.GetAllAsync("any", TestContext.Current.CancellationToken);

			logger.Messages.Should().NotBeEmpty();
			logger.Messages.Should().OnlyContain(message => !message.Contains(Token, StringComparison.Ordinal));
			logger.Messages.Should().Contain(message => message.Contains("X-Rundeck-Auth-Token", StringComparison.Ordinal));
		}

		private sealed class CapturingLogger : ILogger
		{
			public List<string> Messages { get; } = [];

			public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

			public bool IsEnabled(LogLevel logLevel) => true;

			public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
				=> Messages.Add(formatter(state, exception));
		}
	}
}
