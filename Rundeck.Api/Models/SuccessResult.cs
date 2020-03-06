using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class SuccessResult
	{
		[DataMember(Name = "success")]
		public bool Success { get; set; }
	}
}