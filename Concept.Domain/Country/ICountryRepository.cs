namespace Concept.Domain.Country
{
	using Infrastructure.Common.Domain;
	using System.Linq;
	using System.Threading.Tasks;

	public interface ICountryRepository : IRepository<Country, string>
	{
		Task<Country> GetAsync(string id);
		IQueryable<Country> GetAllQuery();
	}
}
