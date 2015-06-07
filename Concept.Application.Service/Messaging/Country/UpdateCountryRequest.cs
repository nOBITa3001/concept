namespace Concept.Application.Service.Messaging.Country
{
	public class UpdateCountryRequest : StringIDRequest
	{
		public CountryPropertiesViewModel CountryProperties { get; set; }

		public UpdateCountryRequest(string id)
			: base(id)
		{
			this.CountryProperties = new CountryPropertiesViewModel();
		}
	}
}