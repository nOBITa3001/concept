namespace Concept.Domain.Continent
{
	using Infrastructure.Common.Domain;
	using System.Linq;
	using System.Threading.Tasks;

	public interface IContinentRepository : IRepository<Continent, string>
	{
		Task<Continent> GetAsync(string id);
		IQueryable<Continent> GetAllQuery();
	}
}
