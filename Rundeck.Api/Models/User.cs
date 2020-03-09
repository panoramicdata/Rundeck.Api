using System;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// A user
	/// </summary>
	[DataContract]
	public class User
	{
		/// <summary>
		/// The login
		/// </summary>
		[DataMember(Name = "login")]
		public string Login { get; set; } = string.Empty;

		/// <summary>
		/// First name
		/// </summary>
		[DataMember(Name = "firstName")]
		public string FirstName { get; set; } = string.Empty;

		/// <summary>
		/// Last name
		/// </summary>
		[DataMember(Name = "lastName")]
		public string LastName { get; set; } = string.Empty;

		/// <summary>
		/// Email address
		/// </summary>
		[DataMember(Name = "email")]
		public string Email { get; set; } = string.Empty;

		/// <summary>
		/// Creation time
		/// </summary>
		[DataMember(Name = "created")]
		public DateTimeOffset Created { get; set; } = DateTimeOffset.MinValue;

		/// <summary>
		/// Last update time
		/// </summary>
		[DataMember(Name = "updated")]
		public DateTimeOffset? Updated { get; set; }

		/// <summary>
		/// The number of tokens
		/// </summary>
		[DataMember(Name = "tokens")]
		public int TokenCount { get; set; }
	}
}
