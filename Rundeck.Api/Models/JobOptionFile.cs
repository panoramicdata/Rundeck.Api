using System;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class JobOptionFile
	{
		[DataMember(Name = "id")]
		public string Id { get; set; } = default!;

		[DataMember(Name = "user")]
		public string User { get; set; } = default!;

		[DataMember(Name = "optionName")]
		public string OptionName { get; set; } = default!;

		[DataMember(Name = "fileState")]
		public string FileState { get; set; } = default!;

		[DataMember(Name = "sha")]
		public string Sha { get; set; } = default!;

		[DataMember(Name = "jobId")]
		public string JobId { get; set; } = default!;

		[DataMember(Name = "dateCreated")]
		public DateTimeOffset DateCreated { get; set; }

		[DataMember(Name = "serverNodeUUID")]
		public string ServerNodeUUID { get; set; } = default!;

		[DataMember(Name = "fileName")]
		public object FileName { get; set; } = default!;

		[DataMember(Name = "size")]
		public int Size { get; set; }

		[DataMember(Name = "expirationDate")]
		public DateTimeOffset ExpirationDate { get; set; }

		[DataMember(Name = "execId")]
		public object ExecId { get; set; } = default!;
	}
}