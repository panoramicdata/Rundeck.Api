using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class AdhocExecutionResult
	{
		[DataMember(Name = "message")]
		public string Message { get; set; } = default!;

		[DataMember(Name = "execution")]
		public Execution Execution { get; set; } = new Execution();
	}
}
