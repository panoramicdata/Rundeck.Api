using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Xunit;

namespace Rundeck.Api.Test
{
	internal static class SerializerTestHelpers
	{
		internal static async Task<T> ReadAsync<T>(string json, Func<RundeckClient, Task<T>> call)
		{
			using var server = new StubRundeckServer(json);
			using var client = server.CreateClient();
			return await call(client);
		}

		internal static Task<Rundeck.Api.Models.Execution> ReadExecutionAsync(string json)
			=> ReadAsync(json, c => c.Executions.GetAsync(1, TestContext.Current.CancellationToken));

		internal static Task<Rundeck.Api.Models.User> ReadUserAsync(string json)
			=> ReadAsync(json, c => c.Users.GetAsync("u", TestContext.Current.CancellationToken));

		internal static async Task<string?> SentBodyAsync(Func<RundeckClient, Task> call, string response = "{}")
		{
			using var server = new StubRundeckServer(response);
			using var client = server.CreateClient();
			await call(client);
			return server.LastRequestBody;
		}

		internal static async Task<TException> ThrownAsync<TException>(Func<Task> act)
			where TException : Exception
		{
			var thrown = (await Assert.ThrowsAnyAsync<Exception>(act)).GetBaseException();
			return Assert.IsType<TException>(thrown, exactMatch: false);
		}

		internal static string Describe(object? value)
			=> value switch
			{
				null => "null",
				string s => $"string:{s}",
				bool b => $"bool:{b}",
				int or long => $"long:{Convert.ToInt64(value, CultureInfo.InvariantCulture)}",
				float or double or decimal => $"double:{Convert.ToDouble(value, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture)}",
				_ => $"other:{value.GetType().Name}"
			};

		internal static string DescribeNested(object? value, string key)
		{
			var inner = value switch
			{
				IDictionary<string, object?> d => d[key],
				{ } o => TokenValue(o.GetType().GetProperty("Item", [typeof(string)])!.GetValue(o, [key])),
				_ => throw new InvalidOperationException($"Unexpected nested type {value?.GetType().Name}")
			};
			return Describe(inner);
		}

		private static object? TokenValue(object? token)
			=> token?.GetType().GetProperty("Value")?.GetValue(token);
	}
}
