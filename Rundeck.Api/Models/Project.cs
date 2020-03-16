using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// A Project
	/// Documentation: https://docs.rundeck.com/docs/administration/projects/
	/// </summary>
	[DataContract]
	public class Project
	{
		/// <summary>
		/// The project description
		/// </summary>
		[DataMember(Name = "description")]
		public string Description { get; set; } = default!;

		/// <summary>
		/// The project name
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; } = default!;

		/// <summary>
		/// The project URL
		/// </summary>
		[DataMember(Name = "url")]
		public string Url { get; set; } = default!;

		/// <summary>
		/// Project configuration.  For more details, see:
		/// https://docs.rundeck.com/docs/administration/projects/configuration.html
		/// </summary>
		[DataMember(Name = "config")]
		public Config Config { get; set; } = new Config();
	}
}
