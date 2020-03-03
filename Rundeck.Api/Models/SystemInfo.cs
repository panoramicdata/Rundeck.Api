using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// The SystemInfo
	/// </summary>
	[DataContract]
	public class SystemInfo
	{
		/// <summary>
		/// The actual SystemNode
		/// </summary>
		[DataMember(Name = "system")]
		public SystemNode System { get; set; } = new SystemNode();
	}
}
