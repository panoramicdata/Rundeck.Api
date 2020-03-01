using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// An int value
	/// </summary>
	[DataContract]
	public class IntValue : ValueObject<int>
	{
	}
}
