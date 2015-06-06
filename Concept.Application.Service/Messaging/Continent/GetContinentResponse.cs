namespace Concept.Application.Service.Messaging.Continent
{
	using Domain.Continent;

	public class GetContinentResponse : ServiceResponseBase
	{
		public Continent Continent { get; set; }
	}
}
