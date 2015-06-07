namespace Concept.Application.Service.Messaging.Country
{
	using Domain.Country;
	using System.Collections.Generic;

	public class GetCountriesResponse : ServiceResponseBase
	{
		public IEnumerable<Country> Countries { get; set; }
	}
}