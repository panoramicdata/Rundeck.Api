﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class BulkActionResult
	{
		[DataMember(Name = "requestCount")]
		public int RequestCount { get; set; }

		[DataMember(Name = "successCount")]
		public int SuccessCount { get; set; }

		[DataMember(Name = "failedCount")]
		public int FailedCount { get; set; }

		[DataMember(Name = "allsuccessful")]
		public bool Allsuccessful { get; set; }

		[DataMember(Name = "enabled")]
		public bool? Enabled { get; set; }

		[DataMember(Name = "succeeded")]
		public IList<ActionResult> Succeeded { get; set; } = new List<ActionResult>();

		[DataMember(Name = "failed")]
		public IList<ActionResult> Failed { get; set; } = new List<ActionResult>();
	}
}
