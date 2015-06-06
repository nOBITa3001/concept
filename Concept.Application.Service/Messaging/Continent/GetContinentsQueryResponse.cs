namespace Concept.Application.Service.Messaging.Continent
{
	using Domain.Continent;
	using System.Linq;

	public class GetContinentsQueryResponse : ServiceResponseBase
	{
		public IQueryable<Continent> Query { get; set; }
	}
}