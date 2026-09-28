using AwesomeAssertions;
using Rundeck.Api.Models;
using Xunit;
using System.Threading.Tasks;

namespace Rundeck.Api.Test.Documented
{
	public class AclPoliciesTests : TestBase
	{
		public AclPoliciesTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async Task Policies_GetAll_Ok()
		{
			var policyListing = await RundeckClient
				.Policies
				.GetAllAsync(TestContext.Current.CancellationToken);

			policyListing.Should().NotBeNull();
			policyListing.Policies.Should().NotBeNull();
			policyListing.Policies.Should().BeEmpty();
		}

		[Fact]
		public async Task Policies_BasicCrud_Ok()
		{
			// Arrange
			// Make sure there are no policies
			var policyListing = await RundeckClient
				.Policies
				.GetAllAsync(TestContext.Current.CancellationToken);

			policyListing.Should().NotBeNull();
			policyListing.Policies.Should().NotBeNull();
			policyListing.Policies.Should().BeEmpty();

			// Act
			const string policyName = "test.aclpolicy";
			try
			{
				// Create a policy
				var policy = await RundeckClient
					.Policies
					.CreateAsync(policyName, new AclPolicy
						{
							Contents = "description: \"my policy\"\ncontext:\n  application: rundeck\nfor:\n  project:\n    - allow: read\nby:\n  group: build"
						}, TestContext.Current.CancellationToken);

				// Assert
				policy.Should().NotBeNull();

				// GetAll
				var allAfterCreate = await RundeckClient
				.Policies
				.GetAllAsync(TestContext.Current.CancellationToken);

				allAfterCreate.Policies.Should().HaveCount(1);
				allAfterCreate.Policies[0].Name.Should().Be(policyName);

				var policyByName = await RundeckClient
				.Policies
				.GetAsync(policyName, TestContext.Current.CancellationToken);

				policyByName.Should().NotBeNull();
				policyByName.Should().BeEquivalentTo(policy);
				policyByName.Contents.Should().Be(policy.Contents);

				// Update
				const string newContents = "description: \"updated policy\"\ncontext:\n  application: rundeck\nfor:\n  project:\n    - allow: read\nby:\n  group: build";
				var updatedPolicy = await RundeckClient
					.Policies
					.UpdateAsync(policyName, new AclPolicy
						{
							Contents = newContents
						}, TestContext.Current.CancellationToken);

				updatedPolicy.Should().NotBeNull();
				updatedPolicy.Should().NotBeEquivalentTo(policy);
				updatedPolicy.Contents.Should().Be(newContents);
			}
			finally
			{
				// Cleanup
				await RundeckClient
					.Policies
					.DeleteAsync(policyName, TestContext.Current.CancellationToken);

				var allAfterDelete = await RundeckClient
				.Policies
				.GetAllAsync(TestContext.Current.CancellationToken);

				allAfterDelete.Should().NotBeNull();
				allAfterDelete.Policies.Should().NotBeNull();
				allAfterDelete.Policies.Should().BeEmpty();
			}
		}
	}
}
