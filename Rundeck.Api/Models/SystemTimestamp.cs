using System;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// The SystemTimeStamp
	/// </summary>
	[DataContract]
	public class SystemTimestamp
	{
		/// <summary>
		/// Epoch time
		/// </summary>
		[DataMember(Name = "epoch")]
		public long Epoch { get; set; }

		/// <summary>
		/// Unit
		/// </summary>
		[DataMember(Name = "unit")]
		public string Unit { get; set; } = default!;

		/// <summary>
		/// DateTimeOffset
		/// </summary>
		[DataMember(Name = "datetime")]
		public DateTimeOffset Datetime { get; set; }
	}
}