using FluentAssertions;
using Refit;
using Rundeck.Api.Models;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

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
				.SetPassiveModeAsync()
				;

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
				.SetActiveModeAsync()
				;

			executionMode.ExecutionModeEnum.Should().Be(ExecutionModeEnum.Active);
		}
	}
}
