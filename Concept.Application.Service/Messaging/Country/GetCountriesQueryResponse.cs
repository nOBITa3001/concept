namespace Concept.Application.Service.Messaging.Country
{
	using Domain.Country;
	using System.Linq;

	public class GetCountriesQueryResponse : ServiceResponseBase
	{
		public IQueryable<Country> Query { get; set; }
	}
}