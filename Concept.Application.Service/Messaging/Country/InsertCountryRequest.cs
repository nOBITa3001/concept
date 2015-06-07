namespace Concept.Application.Service.Messaging.Country
{
	public class InsertCountryRequest : StringIDRequest
	{
		public CountryPropertiesViewModel CountryProperties { get; set; }

		public InsertCountryRequest(string id)
			: base(id)
		{
			this.CountryProperties = new CountryPropertiesViewModel();
		}
	}
}