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
		public async void System_CycleExecutionModeAsync_Passes()
		{
			var executionMode = await RundeckClient
				.System
				.SetPassiveModeAsync()
				.ConfigureAwait(false);

			Func<Task> act = async () =>
			{
				executionMode = await RundeckClient
				.System
				.GetExecutionModeAsync()
				.ConfigureAwait(false);
			};
			await act
				.Should()
				.ThrowAsync<ApiException>()
				.ConfigureAwait(false);

			executionMode = await RundeckClient
				.System
				.SetActiveModeAsync()
				.ConfigureAwait(false);

			executionMode.ExecutionModeEnum.Should().Be(ExecutionModeEnum.Active);
		}
	}
}
