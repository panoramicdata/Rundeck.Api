using System;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// A Project
	/// Documentation: https://docs.rundeck.com/docs/administration/projects/
	/// </summary>
	public class Project
	{
		/// <summary>
		/// The project description
		/// </summary>
		[JsonPropertyName("description")]
		public string Description { get; set; } = default!;

		/// <summary>
		/// The project name
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; } = default!;

		/// <summary>
		/// The project URL
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; } = default!;

		/// <summary>
		/// Created
		/// </summary>
		[JsonPropertyName("created")]
		public DateTimeOffset Created { get; set; }

		/// <summary>
		/// Project configuration.  For more details, see:
		/// https://docs.rundeck.com/docs/administration/projects/configuration.html
		/// </summary>
		[JsonPropertyName("config")]
		public Config Config { get; set; } = new Config();
	}
}
