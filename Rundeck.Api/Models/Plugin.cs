using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Plugin
	/// </summary>
	[DataContract]
	public class Plugin
	{
		/// <summary>
		/// artifactName
		/// </summary>
		[DataMember(Name = "artifactName")]
		public string ArtifactName { get; set; } = string.Empty;

		/// <summary>
		/// Author
		/// </summary>
		[DataMember(Name = "author")]
		public string Author { get; set; } = string.Empty;

		/// <summary>
		/// builtin
		/// </summary>
		[DataMember(Name = "builtin")]
		public bool Builtin { get; set; }

		/// <summary>
		/// Description
		/// </summary>
		[DataMember(Name = "description")]
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// id
		/// </summary>
		[DataMember(Name = "id")]
		public string Id { get; set; } = string.Empty;

		/// <summary>
		/// Name
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// PluginVersion
		/// </summary>
		[DataMember(Name = "pluginVersion")]
		public string PluginVersion { get; set; } = string.Empty;

		/// <summary>
		/// Service
		/// </summary>
		[DataMember(Name = "service")]
		public string Service { get; set; } = string.Empty;

		/// <summary>
		/// Title
		/// </summary>
		[DataMember(Name = "title")]
		public string Title { get; set; } = string.Empty;
	}
}
