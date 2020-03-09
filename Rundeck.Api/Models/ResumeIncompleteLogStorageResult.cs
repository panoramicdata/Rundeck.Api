using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class ResumeIncompleteLogStorageResult
	{
		/// <summary>
		/// Whether it was resumed
		/// </summary>
		[DataMember(Name = "resumed")]
		public bool Resumed { get; set; }
	}
}
