namespace Concept.Application.Service.Messaging.Country
{
	public class GetCountryRequest : StringIDRequest
	{
		public GetCountryRequest(string id)
			: base(id)
		{

		}
	}
}