using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Rundeck info
	/// </summary>
	[DataContract]
	public class RundeckInfo
	{
		/// <summary>
		/// Version
		/// </summary>
		[DataMember(Name = "version")]
		public string Version { get; set; } = default!;

		/// <summary>
		/// Build
		/// </summary>
		[DataMember(Name = "build")]
		public string Build { get; set; } = default!;

		/// <summary>
		/// BuildGit
		/// </summary>
		[DataMember(Name = "buildGit")]
		public string BuildGit { get; set; } = default!;

		/// <summary>
		/// Node
		/// </summary>
		[DataMember(Name = "node")]
		public string Node { get; set; } = default!;

		/// <summary>
		/// Base
		/// </summary>
		[DataMember(Name = "base")]
		public string Base { get; set; } = default!;

		/// <summary>
		/// API version
		/// </summary>
		[DataMember(Name = "apiversion")]
		public int ApiVersion { get; set; }

		/// <summary>
		/// ServerUUID
		/// </summary>
		[DataMember(Name = "serverUUID")]
		public string ServerUUID { get; set; } = default!;
	}
}