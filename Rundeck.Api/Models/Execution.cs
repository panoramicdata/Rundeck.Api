using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// An execution
	/// </summary>
	[DataContract]
	public class Execution
	{
		/// <summary>
		/// Execution ID
		/// </summary>
		[DataMember(Name = "id")]
		public int Id { get; set; }

		/// <summary>
		/// Project Name
		/// </summary>
		[DataMember(Name = "project")]
		public string Project { get; set; } = string.Empty;

		/// <summary>
		/// API URL for Execution
		/// </summary>
		[DataMember(Name = "href")]
		public string Href { get; set; } = string.Empty;

		/// <summary>
		/// GUI URL for Execution
		/// </summary>
		[DataMember(Name = "permalink")]
		public string Permalink { get; set; } = string.Empty;

		/// <summary>
		/// The storage
		/// </summary>
		[DataMember(Name = "storage")]
		public Storage Storage { get; set; } = new Storage();

		/// <summary>
		/// The errors
		/// </summary>
		[DataMember(Name = "errors")]
		public List<string> Errors { get; set; } = new List<string>();
	}
}