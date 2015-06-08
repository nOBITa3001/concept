namespace Concept.Application.Service.Messaging.Country
{
	public class DeleteCountryLanguageRequest : CountryLanguageIDRequest
	{
		public CountryLanguagePropertiesViewModel CountryLanguageProperties { get; set; }

		public DeleteCountryLanguageRequest(string countryId, string languageId)
			: base(countryId, languageId)
		{
			this.CountryLanguageProperties = new CountryLanguagePropertiesViewModel();
		}
	}
}