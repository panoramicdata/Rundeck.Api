using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{

	/// <summary>
	/// An int value
	/// </summary>
	[DataContract]
	public class IntValue
	{
		[DataMember(Name = "value")]
		public int Value { get; set; }
	}

}
