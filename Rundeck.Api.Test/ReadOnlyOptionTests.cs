using AwesomeAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Rundeck.Api.Test
{
	/// <summary>
	/// <see cref="RundeckClientOptions.IsReadOnly"/> needs no Rundeck, credentials or configuration.
	/// </summary>
	public class ReadOnlyOptionTests
	{
		private static RundeckClient ClientFor(Uri uri, bool isReadOnly)
			=> new(
				new RundeckClientOptions { Uri = uri, ApiToken = "not-a-real-token", IsReadOnly = isReadOnly },
				NullLogger.Instance);

		private static bool IsRefused(Exception e)
			=> (e is InvalidOperationException || e.InnerException is InvalidOperationException)
			&& e.ToString().Contains("read-only", StringComparison.Ordinal);

		private static Uri FreeLoopbackUri()
		{
			var probe = new TcpListener(IPAddress.Loopback, 0);
			probe.Start();
			var port = ((IPEndPoint)probe.LocalEndpoint).Port;
			probe.Stop();
			return new Uri($"http://127.0.0.1:{port}/");
		}

		/// <summary>The option is off unless asked for.</summary>
		[Fact]
		public void IsReadOnly_DefaultsToFalse()
			=> new RundeckClientOptions().IsReadOnly.Should().BeFalse();

		/// <summary>A write is refused before any connection is made (nothing listens on the address).</summary>
		[Fact]
		public async Task ReadOnly_Write_IsRefusedBeforeSending()
		{
			using var client = ClientFor(FreeLoopbackUri(), isReadOnly: true);

			var act = () => client.Jobs.DisableExecutionsAsync("job-1", TestContext.Current.CancellationToken);

			var thrown = await act.Should().ThrowAsync<Exception>();
			IsRefused(thrown.Which).Should().BeTrue();
		}

		/// <summary>With the option off the same write is attempted and fails on the missing server, not on the policy.</summary>
		[Fact]
		public async Task NotReadOnly_Write_IsAttempted()
		{
			using var client = ClientFor(FreeLoopbackUri(), isReadOnly: false);

			var act = () => client.Jobs.DisableExecutionsAsync("job-1", TestContext.Current.CancellationToken);

			var thrown = await act.Should().ThrowAsync<Exception>();
			IsRefused(thrown.Which).Should().BeFalse();
		}

		/// <summary>A read still reaches the server.</summary>
		[Fact]
		public async Task ReadOnly_Read_StillGoesThrough()
		{
			var uri = FreeLoopbackUri();
			using var listener = new HttpListener();
			listener.Prefixes.Add(uri.AbsoluteUri);
			listener.Start();
			var served = Task.Run(async () =>
			{
				var context = await listener.GetContextAsync();
				var bytes = Encoding.UTF8.GetBytes("[]");
				context.Response.ContentType = "application/json";
				context.Response.ContentLength64 = bytes.Length;
				await context.Response.OutputStream.WriteAsync(bytes, TestContext.Current.CancellationToken);
				context.Response.Close();
			}, TestContext.Current.CancellationToken);
			using var client = ClientFor(uri, isReadOnly: true);

			var jobs = await client.Jobs.GetAllAsync("any", TestContext.Current.CancellationToken);

			jobs.Should().BeEmpty();
			await served;
		}
	}
}
