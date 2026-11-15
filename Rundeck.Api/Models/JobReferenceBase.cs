using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// The identifying details shared by a job and the result of importing one
	/// </summary>
	[DataContract]
	public abstract class JobReferenceBase
	{
		[DataMember(Name = "id")]
		public string Id { get; set; } = default!;

		[DataMember(Name = "name")]
		public string Name { get; set; } = default!;

		[DataMember(Name = "group")]
		public string Group { get; set; } = default!;

		[DataMember(Name = "project")]
		public string Project { get; set; } = default!;

		[DataMember(Name = "href")]
		public string Href { get; set; } = default!;

		[DataMember(Name = "permalink")]
		public string Permalink { get; set; } = default!;
	}
}
