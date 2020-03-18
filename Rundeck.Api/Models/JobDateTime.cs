using System;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class JobDateTime
	{
		[DataMember(Name = "unixtime")]
		public long Unixtime { get; set; }

		[DataMember(Name = "date")]
		public DateTimeOffset Date { get; set; }
	}
}