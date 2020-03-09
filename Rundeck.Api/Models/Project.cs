using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// A Project
	/// </summary>
	[DataContract]
	public class Project
	{
		/// <summary>
		/// Description
		/// </summary>
		[DataMember(Name = "description")]
		public string Description { get; set; } = default!;

		/// <summary>
		/// Name
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; } = default!;

		/// <summary>
		/// Url
		/// </summary>
		[DataMember(Name = "url")]
		public string Url { get; set; } = default!;

		/// <summary>
		/// Config
		/// </summary>
		[DataMember(Name = "config")]
		public Config Config { get; set; } = new Config();
	}
}
