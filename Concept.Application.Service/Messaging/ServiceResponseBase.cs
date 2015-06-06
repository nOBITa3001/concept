namespace Concept.Application.Service.Messaging
{
	using System;

	public abstract class ServiceResponseBase
	{
		#region Properties

		/// <summary>
		/// Save the exception thrown so that consumers can read it.
		/// </summary>
		public Exception Exception { get; set; }

		#endregion
	}
}
