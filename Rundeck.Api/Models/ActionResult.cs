using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class ActionResult
	{
		[DataMember(Name = "id")]
		public string Id { get; set; } = default!;

		[DataMember(Name = "message")]
		public string Message { get; set; } = default!;

		[DataMember(Name = "errorCode")]
		public string? ErrorCode { get; set; }
	}
}