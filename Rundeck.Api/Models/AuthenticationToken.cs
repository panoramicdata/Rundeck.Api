﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// An authentication token
	/// </summary>
	[DataContract]
	public class AuthenticationToken
	{
		/// <summary>
		/// User
		/// </summary>
		[DataMember(Name = "user")]
		public string User { get; set; } = string.Empty;

		/// <summary>
		/// Id
		/// </summary>
		[DataMember(Name = "id")]
		public string Id { get; set; } = string.Empty;

		/// <summary>
		/// Creator
		/// </summary>
		[DataMember(Name = "creator")]
		public string Creator { get; set; } = string.Empty;

		/// <summary>
		/// Expiration
		/// </summary>
		[DataMember(Name = "expiration")]
		public DateTimeOffset Expiration { get; set; }

		/// <summary>
		/// Roles
		/// </summary>
		[DataMember(Name = "roles")]
		public List<string> Roles { get; set; } = new List<string>();

		/// <summary>
		/// Expired
		/// </summary>
		[DataMember(Name = "expired")]
		public bool Expired { get; set; }

		/// <summary>
		/// Token
		/// </summary>
		[DataMember(Name = "token")]
		public string Token { get; set; } = string.Empty;
	}
}
