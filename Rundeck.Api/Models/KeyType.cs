using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	public enum KeyType
	{
		/// <summary>
		/// Default type is password
		/// </summary>
		Password,

		/// <summary>
		/// Public
		/// </summary>
		[EnumMember(Value = "public")]
		Public,

		/// <summary>
		/// Private
		/// </summary>
		[EnumMember(Value = "private")]
		Private
	}
}
