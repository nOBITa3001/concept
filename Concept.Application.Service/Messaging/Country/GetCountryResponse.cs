namespace Concept.Application.Service.Messaging.Country
{
	using Domain.Country;

	public class GetCountryResponse : ServiceResponseBase
	{
		public Country Country { get; set; }
	}
}