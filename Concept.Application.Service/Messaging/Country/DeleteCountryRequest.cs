namespace Concept.Application.Service.Messaging.Country
{
	public class DeleteCountryRequest : StringIDRequest
	{
		public CountryPropertiesViewModel CountryProperties { get; set; }

		public DeleteCountryRequest(string id)
			: base(id)
		{
			this.CountryProperties = new CountryPropertiesViewModel();
		}
	}
}