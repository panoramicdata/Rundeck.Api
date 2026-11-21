using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// The SystemInfo
	/// </summary>
	public class SystemInfo
	{
		/// <summary>
		/// The actual SystemNode
		/// </summary>
		[JsonPropertyName("system")]
		public SystemNode System { get; set; } = new SystemNode();
	}
}
