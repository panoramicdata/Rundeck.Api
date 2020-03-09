using System;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models.Dtos
{
	/// <summary>
	/// A Project when listing Projects
	/// Projects have different properties when listing
	/// </summary>
	[DataContract]
	public class ProjectListingDto : Project
	{
		/// <summary>
		/// Label
		/// </summary>
		[DataMember(Name = "label")]
		public string Label { get; set; } = default!;

		/// <summary>
		/// Created
		/// </summary>
		[DataMember(Name = "created")]
		public DateTimeOffset Created { get; set; }
	}
}
