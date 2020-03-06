using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	public enum KeyResourceType
	{
		/// <summary>
		/// Unknown - useful when deserialising
		/// </summary>
		Unknown,

		/// <summary>
		/// File
		/// </summary>
		[EnumMember(Value = "file")]
		File,

		/// <summary>
		/// Directory
		/// </summary>
		[EnumMember(Value = "directory")]
		Directory
	}
}
