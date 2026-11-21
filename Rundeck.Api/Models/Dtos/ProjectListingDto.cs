using System.Text.Json.Serialization;

namespace Rundeck.Api.Models.Dtos
{
	/// <summary>
	/// A Project when listing Projects
	/// Projects have different properties when listing
	/// </summary>
	public class ProjectListingDto : Project
	{
		/// <summary>
		/// Label
		/// </summary>
		[JsonPropertyName("label")]
		public string Label { get; set; } = default!;
	}
}
