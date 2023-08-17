﻿using FluentAssertions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Rundeck.Api.Test.Documented
{
	public class PluginsTests : TestBase
	{
		public PluginsTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async Task Plugins_GetAll_Passes()
		{
			var plugins = await RundeckClient
				.Plugins
				.GetAllAsync()
				.ConfigureAwait(false);

			plugins.Should().NotBeNull();
		}
	}
}
