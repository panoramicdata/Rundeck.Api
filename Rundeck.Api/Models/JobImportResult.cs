using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class JobImportResult
	{
		[DataMember(Name = "index")]
		public int Index { get; set; }

		[DataMember(Name = "href")]
		public string Href { get; set; } = default!;

		[DataMember(Name = "id")]
		public string Id { get; set; } = default!;

		[DataMember(Name = "name")]
		public string Name { get; set; } = default!;

		[DataMember(Name = "group")]
		public string Group { get; set; } = default!;

		[DataMember(Name = "project")]
		public string Project { get; set; } = default!;

		[DataMember(Name = "permalink")]
		public string Permalink { get; set; } = default!;

		[DataMember(Name = "error")]
		public string? Error { get; set; }
	}
}
