using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class ResumeIncompleteLogStorageResult
	{
		/// <summary>
		/// Whether it was resumed
		/// </summary>
		[JsonPropertyName("resumed")]
		public bool Resumed { get; set; }
	}
}
