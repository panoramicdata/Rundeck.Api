using System.Runtime.Serialization;

namespace Rundeck.Api
{
	/// <summary>
	/// An API error, as sent back from the server
	/// </summary>
	[DataContract]
	public class RundeckError
	{
		/// <summary>
		/// Whether this is an error
		/// </summary>
		[DataMember(Name = "error")]
		public bool Error { get; set; }

		/// <summary>
		/// The API version
		/// </summary>
		[DataMember(Name = "apiversion")]
		public int ApiVersion { get; set; }

		/// <summary>
		/// The error code
		/// </summary>
		[DataMember(Name = "errorCode")]
		public string ErrorCode { get; set; } = string.Empty;

		/// <summary>
		/// The message
		/// </summary>
		[DataMember(Name = "message")]
		public string Message { get; set; } = string.Empty;
	}

}