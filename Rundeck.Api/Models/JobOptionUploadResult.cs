using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class JobOptionUploadResult
	{
		[DataMember(Name = "options")]
		public Dictionary<string, string> Options { get; set; } = new Dictionary<string, string>();

		[DataMember(Name = "total")]
		public int Total { get; set; }
	}
}