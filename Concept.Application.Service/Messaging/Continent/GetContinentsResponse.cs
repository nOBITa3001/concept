namespace Concept.Application.Service.Messaging.Continent
{
	using Domain.Continent;
	using System.Collections.Generic;

	public class GetContinentsResponse : ServiceResponseBase
	{
		public IEnumerable<Continent> Continents { get; set; }
	}
}
