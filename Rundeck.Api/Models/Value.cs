using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public abstract class ValueObject<T> where T : struct
	{
		/// <summary>
		/// The value
		/// </summary>
		[JsonPropertyName("value")]
		public T Value { get; set; } = default;

		public override string ToString()
			=> Value.ToString() ?? string.Empty;
	}
}