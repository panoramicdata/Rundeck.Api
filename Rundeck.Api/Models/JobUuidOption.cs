using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	public enum JobUuidOption
	{
		[EnumMember(Value = "preserve")]
		Preserve,

		[EnumMember(Value = "remove")]
		Remove
	}
}
