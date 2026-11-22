using AwesomeAssertions;
using Rundeck.Api.Models;
using System;
using System.Threading.Tasks;
using Xunit;
using static Rundeck.Api.Test.SerializerTestHelpers;

namespace Rundeck.Api.Test
{
	public partial class SerializerCharacterizationTests
	{
		[Fact]
		public async Task Enum_ReadsEnumMemberValuesCaseInsensitivelyAndNumbers()
		{
			var passive = await ReadAsync("""{"executionMode":"passive"}""", c => c.System.GetExecutionModeAsync(TestContext.Current.CancellationToken));
			var upper = await ReadAsync("""{"executionMode":"ACTIVE"}""", c => c.System.GetExecutionModeAsync(TestContext.Current.CancellationToken));
			var numeric = await ReadAsync("""{"executionMode":2}""", c => c.System.GetExecutionModeAsync(TestContext.Current.CancellationToken));
			var byName = await ReadAsync("""{"executionMode":"Passive"}""", c => c.System.GetExecutionModeAsync(TestContext.Current.CancellationToken));

			passive.ExecutionModeEnum.Should().Be(ExecutionModeEnum.Passive);
			upper.ExecutionModeEnum.Should().Be(ExecutionModeEnum.Active);
			numeric.ExecutionModeEnum.Should().Be(ExecutionModeEnum.Active);
			byName.ExecutionModeEnum.Should().Be(ExecutionModeEnum.Passive);
		}

		[Fact]
		public async Task Enum_UnknownValue_IsRejected()
		{
			var act = () => ReadAsync("""{"executionMode":"bogus"}""", c => c.System.GetExecutionModeAsync(TestContext.Current.CancellationToken));

			await act.Should().ThrowAsync<Exception>();
		}

		[Fact]
		public async Task Enum_NullForNonNullableEnum_LeavesTheDefault()
		{
			var mode = await ReadAsync("""{"executionMode":null}""", c => c.System.GetExecutionModeAsync(TestContext.Current.CancellationToken));
			mode.ExecutionModeEnum.Should().Be(ExecutionModeEnum.Unknown);
		}
	}
}
