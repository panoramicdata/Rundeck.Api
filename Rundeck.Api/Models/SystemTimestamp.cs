using System;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// The SystemTimeStamp
	/// </summary>
	public class SystemTimestamp
	{
		/// <summary>
		/// Epoch time
		/// </summary>
		[JsonPropertyName("epoch")]
		public long Epoch { get; set; }

		/// <summary>
		/// Unit
		/// </summary>
		[JsonPropertyName("unit")]
		public string Unit { get; set; } = default!;

		/// <summary>
		/// DateTimeOffset
		/// </summary>
		[JsonPropertyName("datetime")]
		public DateTimeOffset Datetime { get; set; }
	}
}