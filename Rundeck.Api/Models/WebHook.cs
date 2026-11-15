using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class WebHook : WebHookBase
	{
		[DataMember(Name = "authToken")]
		public string AuthToken { get; set; } = default!;

		[DataMember(Name = "creator")]
		public string Creator { get; set; } = default!;

		[DataMember(Name = "id")]
		public int Id { get; set; }

		[DataMember(Name = "uuid")]
		public string Uuid { get; set; } = default!;

		/// <summary>
		/// Whether callers must send the auth token in the Authorization header
		/// </summary>
		[DataMember(Name = "useAuth")]
		public bool? UseAuth { get; set; }

		/// <summary>
		/// Whether to generate a new Authorization header secret
		/// </summary>
		[DataMember(Name = "regenAuth")]
		public bool? RegenAuth { get; set; }
	}
}
