using System;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class KeyMeta
	{
		[DataMember(Name = "Rundeck-key-type")]
		public KeyType RundeckKeyType { get; set; } = default!;

		[DataMember(Name = "Rundeck-content-mask")]
		public string RundeckContentMask { get; set; } = default!;

		[DataMember(Name = "Rundeck-content-size")]
		public string RundeckContentSize { get; set; } = default!;

		[DataMember(Name = "Rundeck-content-type")]
		public string RundeckContentType { get; set; } = default!;

		[DataMember(Name = "Rundeck-content-creation-time")]
		public DateTime RundeckContentCreationTime { get; set; }

		[DataMember(Name = "Rundeck-content-modify-time")]
		public DateTime RundeckContentModifyTime { get; set; }

		[DataMember(Name = "Rundeck-auth-created-username")]
		public string RundeckAuthCreatedUsername { get; set; } = default!;

		[DataMember(Name = "Rundeck-auth-modified-username")]
		public string RundeckAuthModifiedUsername { get; set; } = default!;
	}
}
