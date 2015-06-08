namespace Concept.Application.Service.Messaging.Country
{
	public class InsertCountryLanguageRequest : CountryLanguageIDRequest
	{
		public CountryLanguagePropertiesViewModel CountryLanguageProperties { get; set; }

		public InsertCountryLanguageRequest(string countryId, string languageId)
			: base(countryId, languageId)
		{
			this.CountryLanguageProperties = new CountryLanguagePropertiesViewModel();
		}
	}
}