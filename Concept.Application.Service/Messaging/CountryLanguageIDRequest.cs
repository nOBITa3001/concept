namespace Concept.Application.Service.Messaging
{
	using Domain.Country;
	using System;

	public abstract class CountryLanguageIDRequest : ServiceRequestBase
	{
		public CountryLanguageID ID { get; private set; }

		public CountryLanguageIDRequest(string countryId, string languageId)
		{
			this.ThrowExceptionIfRequestIsInvalid(countryId, languageId);

			this.ID = new CountryLanguageID()
			{
				CountryID = countryId
				, LanguageID = languageId
			};
		}

		private void ThrowExceptionIfRequestIsInvalid(string countryId, string languageId)
		{
			if (string.IsNullOrWhiteSpace(countryId))
			{
				throw new ArgumentException("Country ID must not be null, empty or whitespace.");
			}
			if (string.IsNullOrWhiteSpace(languageId))
			{
				throw new ArgumentException("Language ID must not be null, empty or whitespace.");
			}
		}
	}
}
