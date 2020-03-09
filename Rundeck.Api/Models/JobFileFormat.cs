using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	public enum JobFileFormat
	{
		[EnumMember(Value = "xml")]
		XML,

		[EnumMember(Value = "yaml")]
		YAML
	}
}