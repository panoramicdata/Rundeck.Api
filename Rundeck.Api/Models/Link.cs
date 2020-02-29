using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Link
	/// </summary>
	[DataContract]
	public class Link
	{
		/// <summary>
		/// The HREF
		/// </summary>
		[DataMember(Name = "href")]
		public string Href { get; set; } = default!;
	}
}
