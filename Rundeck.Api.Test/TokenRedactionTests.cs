using AwesomeAssertions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
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
			var (logger, messages) = CapturingLogger.Create();
			using var client = new RundeckClient(
				new RundeckClientOptions { Uri = server.Uri, ApiToken = Token, Logger = logger });

			await client.Jobs.GetAllAsync("any", TestContext.Current.CancellationToken);

			messages.Should().NotBeEmpty();
			messages.Should().OnlyContain(message => !message.Contains(Token, StringComparison.Ordinal));
			messages.Should().Contain(message => message.Contains("X-Rundeck-Auth-Token", StringComparison.Ordinal));
		}

		public class CapturingLogger : DispatchProxy
		{
			public List<string> Messages { get; } = [];

			public static (ILogger Logger, List<string> Messages) Create()
			{
				var logger = Create<ILogger, CapturingLogger>();
				return (logger, ((CapturingLogger)(object)logger).Messages);
			}

			protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
			{
				if (targetMethod?.Name == nameof(ILogger.IsEnabled))
				{
					return true;
				}

				if (targetMethod?.Name == nameof(ILogger.Log))
				{
					var formatter = (Delegate)args![4]!;
					Messages.Add((string)formatter.DynamicInvoke(args[2], args[3])!);
				}

				return null;
			}
		}
	}
}
