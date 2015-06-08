namespace Concept.Application.Service.Messaging.Country
{
	using Domain.Country;
	using System.Linq;

	public class GetCountryLanguagesQueryResponse : ServiceResponseBase
	{
		public IQueryable<CountryLanguage> Query { get; set; }
	}
}