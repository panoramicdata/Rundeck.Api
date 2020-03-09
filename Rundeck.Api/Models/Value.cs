using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public abstract class ValueObject<T> where T : struct
	{
		/// <summary>
		/// The value
		/// </summary>
		[DataMember(Name = "value")]
		public T Value { get; set; } = default;

		public override string ToString()
			=> Value.ToString();
	}
}