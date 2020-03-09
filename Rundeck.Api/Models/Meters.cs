using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Meters
	/// </summary>
	[DataContract]
	public class Meters
	{
		/// <summary>
		/// RundeckControllersFrameworkControllerCreateProjectPost
		/// </summary>
		[DataMember(Name = "rundeck.controllers.FrameworkController.createProjectPost")]
		public Meter RundeckControllersFrameworkControllerCreateProjectPost { get; set; } = new Meter();

		/// <summary>
		/// RundeckServicesAuthorizationServicesystemAuthorizationEvaluateMeter
		/// </summary>
		[DataMember(Name = "rundeck.services.AuthorizationService.systemAuthorization.evaluateMeter")]
		public Meter RundeckServicesAuthorizationServicesystemAuthorizationEvaluateMeter { get; set; } = new Meter();

		/// <summary>
		/// RundeckServicesAuthorizationServicesystemAuthorizationEvaluateSetMeter
		/// </summary>
		[DataMember(Name = "rundeck.services.AuthorizationService.systemAuthorization.evaluateSetMeter")]
		public Meter RundeckServicesAuthorizationServicesystemAuthorizationEvaluateSetMeter { get; set; } = new Meter();

		/// <summary>
		/// RundeckServicesExecutionServiceExecutionJobStartMeter
		/// </summary>
		[DataMember(Name = "rundeck.services.ExecutionService.executionJobStartMeter")]
		public Meter RundeckServicesExecutionServiceExecutionJobStartMeter { get; set; } = new Meter();

		/// <summary>
		/// RundeckServicesExecutionServiceExecutionStartMeter
		/// </summary>
		[DataMember(Name = "rundeck.services.ExecutionService.executionStartMeter")]
		public Meter RundeckServicesExecutionServiceExecutionStartMeter { get; set; } = new Meter();

		/// <summary>
		/// RundeckServicesExecutionServiceExecutionSuccessMeter
		/// </summary>
		[DataMember(Name = "rundeck.services.ExecutionService.executionSuccessMeter")]
		public Meter RundeckServicesExecutionServiceExecutionSuccessMeter { get; set; } = new Meter();
	}
}
