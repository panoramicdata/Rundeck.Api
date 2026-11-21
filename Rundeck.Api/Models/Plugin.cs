using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Plugin
	/// </summary>
	public class Plugin
	{
		/// <summary>
		/// artifactName
		/// </summary>
		[JsonPropertyName("artifactName")]
		public string ArtifactName { get; set; } = string.Empty;

		/// <summary>
		/// Author
		/// </summary>
		[JsonPropertyName("author")]
		public string Author { get; set; } = string.Empty;

		/// <summary>
		/// builtin
		/// </summary>
		[JsonPropertyName("builtin")]
		public bool Builtin { get; set; }

		/// <summary>
		/// Description
		/// </summary>
		[JsonPropertyName("description")]
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// id
		/// </summary>
		[JsonPropertyName("id")]
		public string Id { get; set; } = string.Empty;

		/// <summary>
		/// Name
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// PluginVersion
		/// </summary>
		[JsonPropertyName("pluginVersion")]
		public string PluginVersion { get; set; } = string.Empty;

		/// <summary>
		/// Service
		/// </summary>
		[JsonPropertyName("service")]
		public string Service { get; set; } = string.Empty;

		/// <summary>
		/// Title
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; } = string.Empty;
	}
}
