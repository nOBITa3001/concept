namespace Concept.Infrastructure.Common.Domain
{
	using System;

	public class ValueObjectIsInvalidException : Exception
	{
		#region Constructors

		/// <summary>
		/// Initialize a ValueObjectIsInvalidException class.
		/// </summary>
		public ValueObjectIsInvalidException(string message)
			: base(message) { }

		#endregion
	}
}
