using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Meters
	/// </summary>
	[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Skip)]
	public class Meters
	{
		/// <summary>
		/// RundeckControllersFrameworkControllerCreateProjectPost
		/// </summary>
		[JsonPropertyName("rundeck.controllers.FrameworkController.createProjectPost")]
		public Meter RundeckControllersFrameworkControllerCreateProjectPost { get; set; } = new Meter();

		/// <summary>
		/// RundeckServicesAuthorizationServicesystemAuthorizationEvaluateMeter
		/// </summary>
		[JsonPropertyName("rundeck.services.AuthorizationService.systemAuthorization.evaluateMeter")]
		public Meter RundeckServicesAuthorizationServicesystemAuthorizationEvaluateMeter { get; set; } = new Meter();

		/// <summary>
		/// RundeckServicesAuthorizationServicesystemAuthorizationEvaluateSetMeter
		/// </summary>
		[JsonPropertyName("rundeck.services.AuthorizationService.systemAuthorization.evaluateSetMeter")]
		public Meter RundeckServicesAuthorizationServicesystemAuthorizationEvaluateSetMeter { get; set; } = new Meter();

		/// <summary>
		/// RundeckServicesExecutionServiceExecutionAdhocStartMeter
		/// </summary>
		[JsonPropertyName("rundeck.services.ExecutionService.executionAdhocStartMeter")]
		public Meter RundeckServicesExecutionServiceExecutionAdhocStartMeter { get; set; } = new Meter();

		/// <summary>
		/// RundeckServicesExecutionServiceExecutionJobStartMeter
		/// </summary>
		[JsonPropertyName("rundeck.services.ExecutionService.executionJobStartMeter")]
		public Meter RundeckServicesExecutionServiceExecutionJobStartMeter { get; set; } = new Meter();

		/// <summary>
		/// RundeckServicesExecutionServiceExecutionStartMeter
		/// </summary>
		[JsonPropertyName("rundeck.services.ExecutionService.executionStartMeter")]
		public Meter RundeckServicesExecutionServiceExecutionStartMeter { get; set; } = new Meter();

		/// <summary>
		/// RundeckServicesExecutionServiceExecutionSuccessMeter
		/// </summary>
		[JsonPropertyName("rundeck.services.ExecutionService.executionSuccessMeter")]
		public Meter RundeckServicesExecutionServiceExecutionSuccessMeter { get; set; } = new Meter();

		/// <summary>
		/// Meters not named above; the names vary by server version and usage
		/// </summary>
		[JsonExtensionData]
		public IDictionary<string, object> AdditionalMetrics { get; set; } = new Dictionary<string, object>();
	}
}
