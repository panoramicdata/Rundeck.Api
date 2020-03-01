using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Rundeck.Api.Test
{
	public class MetricsTests : TestBase
	{
		public MetricsTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async void Metrics_GetAsync_Passes()
		{
			var metrics = await RundeckClient
				.Metrics
				.GetAsync()
				.ConfigureAwait(false);

			metrics.Should().NotBeNull();
			metrics.Links.Should().NotBeNull();
			metrics.Links.Metrics.Should().NotBeNull();
			metrics.Links.Ping.Should().NotBeNull();
			metrics.Links.Healthcheck.Should().NotBeNull();
			metrics.Links.Threads.Should().NotBeNull();
		}

		[Fact]
		public async void Metrics_GetMetricsAsync_Passes()
		{
			var metrics = await RundeckClient
				.Metrics
				.GetMetricsAsync()
				.ConfigureAwait(false);

			metrics.Should().NotBeNull();
		}
	}
}
