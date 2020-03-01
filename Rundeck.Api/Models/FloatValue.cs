using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{

	/// <summary>
	/// Float value
	/// </summary>
	[DataContract]
	public class FloatValue
	{
		[DataMember(Name = "value")]
		public float Value { get; set; }
	}

}
