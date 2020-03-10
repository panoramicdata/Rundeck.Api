using System;

namespace Rundeck.Api.Interfaces
{
	/// <summary>
	/// The version of the API that the endpoint was introudcued in.
	/// If set, the API will throw a NotSupportedException if the
	/// version of Rundeck being queried is lower than this number.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	internal class IntroducedInVersionAttribute : Attribute
	{
		public int InitialVersion { get; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="version"></param>
		public IntroducedInVersionAttribute(int version)
		{
			InitialVersion = version;
		}
	}
}