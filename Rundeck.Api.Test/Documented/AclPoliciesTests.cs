using AwesomeAssertions;
using Rundeck.Api.Models;
using Xunit;
using System.Threading.Tasks;

namespace Rundeck.Api.Test.Documented
{
	public class AclPoliciesTests : TestBase
	{
		private const string PolicyName = "test.aclpolicy";

		public AclPoliciesTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public Task Policies_GetAll_Ok()
			=> AssertNoPoliciesAsync();

		[Fact]
		public async Task Policies_BasicCrud_Ok()
		{
			// Make sure there are no policies
			await AssertNoPoliciesAsync();

			try
			{
				var policy = await RundeckClient
					.Policies
					.CreateAsync(PolicyName, PolicyWithDescription("my policy"), TestContext.Current.CancellationToken);

				policy.Should().NotBeNull();

				var allAfterCreate = await RundeckClient
					.Policies
					.GetAllAsync(TestContext.Current.CancellationToken);

				allAfterCreate.Policies.Should().HaveCount(1);
				allAfterCreate.Policies[0].Name.Should().Be(PolicyName);

				var policyByName = await RundeckClient
					.Policies
					.GetAsync(PolicyName, TestContext.Current.CancellationToken);

				policyByName.Should().NotBeNull();
				policyByName.Should().BeEquivalentTo(policy);
				policyByName.Contents.Should().Be(policy.Contents);

				var updatedPolicy = await RundeckClient
					.Policies
					.UpdateAsync(PolicyName, PolicyWithDescription("updated policy"), TestContext.Current.CancellationToken);

				updatedPolicy.Should().NotBeNull();
				updatedPolicy.Should().NotBeEquivalentTo(policy);
				updatedPolicy.Contents.Should().Be(PolicyWithDescription("updated policy").Contents);
			}
			finally
			{
				await RundeckClient
					.Policies
					.DeleteAsync(PolicyName, TestContext.Current.CancellationToken);

				await AssertNoPoliciesAsync();
			}
		}

		private static AclPolicy PolicyWithDescription(string description)
			=> new()
			{
				Contents = $"description: \"{description}\"\ncontext:\n  application: rundeck\nfor:\n  project:\n    - allow: read\nby:\n  group: build"
			};

		private async Task AssertNoPoliciesAsync()
		{
			var policyListing = await RundeckClient
				.Policies
				.GetAllAsync(TestContext.Current.CancellationToken);

			policyListing.Should().NotBeNull();
			policyListing.Policies.Should().NotBeNull();
			policyListing.Policies.Should().BeEmpty();
		}
	}
}
