using FluentAssertions;
using Rundeck.Api.Models;
using Xunit;
using Xunit.Abstractions;

namespace Rundeck.Api.Test.Documented
{
	public class ProjectsTests : TestBase
	{
		public ProjectsTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async void Create_Ok()
		{
			var projects = await RundeckClient
				.Projects
				.GetAllAsync()
				.ConfigureAwait(false);

			projects.Should().NotBeNull();
			projects.Should().BeEmpty();

			try
			{
				var project = await RundeckClient
					.Projects
					.CreateAsync(new Project
					{
						Description = "Test project",
						Name = "Test",
						Url = "example.com",
						Config = new Config()
					}
					)
					.ConfigureAwait(false);

				project.Should().NotBeNull();
			}
			finally
			{
				await RundeckClient
					.Projects
					.DeleteAsync("Test")
					.ConfigureAwait(false);
			}
		}
	}
}
