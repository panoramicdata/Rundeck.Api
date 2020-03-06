using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	[SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "<Pending>")]
	public class KeyStorage
	{
		[DataMember(Name = "resources")]
		public List<KeyResource> Resources { get; set; } = new List<KeyResource>();

		[DataMember(Name = "url")]
		public string Url { get; set; } = default!;

		[DataMember(Name = "type")]
		public KeyResourceType Type { get; set; } = default!;

		[DataMember(Name = "path")]
		public string Path { get; set; } = default!;

		[DataMember(Name = "name")]
		public string Name { get; set; } = default!;

		[DataMember(Name = "meta")]
		public KeyMeta Meta { get; set; } = new KeyMeta();
	}
}
