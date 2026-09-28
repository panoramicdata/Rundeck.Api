using AwesomeAssertions;
using Refit;
using Rundeck.Api.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Rundeck.Api.Test.Documented
{
	public class ExecutionModeTests : TestBase
	{
		public ExecutionModeTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async Task System_CycleExecutionModeAsync_Passes()
		{
			var executionMode = await RundeckClient
				.System
				.SetPassiveModeAsync(TestContext.Current.CancellationToken);

			Func<Task> act = async () =>
			{
				executionMode = await RundeckClient
				.System
				.GetExecutionModeAsync()
				;
			};
			await act
				.Should()
				.ThrowAsync<ApiException>()
				;

			executionMode = await RundeckClient
				.System
				.SetActiveModeAsync(TestContext.Current.CancellationToken);

			executionMode.ExecutionModeEnum.Should().Be(ExecutionModeEnum.Active);
		}
	}
}
