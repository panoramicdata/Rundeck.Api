using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Rundeck.Api.Test
{
	/// <summary>A loopback server returning one canned response and recording the last request, so serializer behaviour is tested end to end through Refit.</summary>
	internal sealed class StubRundeckServer : IDisposable
	{
		private readonly HttpListener _listener = new();
		private readonly Task _loop;
		private readonly int _status;
		private readonly string _responseBody;

		public Uri Uri { get; }

		public string? LastRequestBody { get; private set; }

		public string? LastRequestPath { get; private set; }

		public StubRundeckServer(string responseBody, int status = 200)
		{
			_responseBody = responseBody;
			_status = status;
			var probe = new TcpListener(IPAddress.Loopback, 0);
			probe.Start();
			var port = ((IPEndPoint)probe.LocalEndpoint).Port;
			probe.Stop();
			Uri = new Uri($"http://127.0.0.1:{port}/");
			_listener.Prefixes.Add(Uri.ToString());
			_listener.Start();
			_loop = Task.Run(ServeAsync);
		}

		public RundeckClient CreateClient()
			=> new(new RundeckClientOptions { Uri = Uri, ApiToken = "not-a-real-token" });

		private async Task ServeAsync()
		{
			while (_listener.IsListening)
			{
				HttpListenerContext context;
				try
				{
					context = await _listener.GetContextAsync();
				}
				catch (Exception e) when (e is HttpListenerException or ObjectDisposedException or InvalidOperationException)
				{
					return;
				}

				using var reader = new StreamReader(context.Request.InputStream, Encoding.UTF8);
				LastRequestBody = await reader.ReadToEndAsync();
				LastRequestPath = context.Request.Url?.AbsolutePath;
				var bytes = Encoding.UTF8.GetBytes(_responseBody);
				context.Response.StatusCode = _status;
				context.Response.ContentType = "application/json";
				context.Response.ContentLength64 = bytes.Length;
				await context.Response.OutputStream.WriteAsync(bytes);
				context.Response.Close();
			}
		}

		public void Dispose()
		{
			_listener.Close();
			_loop.Wait(TimeSpan.FromSeconds(5));
		}
	}
}
