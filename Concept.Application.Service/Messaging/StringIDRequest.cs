namespace Concept.Application.Service.Messaging
{
	using System;

	public abstract class StringIDRequest : ServiceRequestBase
	{
		private string id;

		public StringIDRequest(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				throw new ArgumentException("ID must not be null, empty or whitespace.");
			}

			this.id = id;
		}

		public string ID
		{
			get
			{
				return this.id;
			}
		}
	}
}
