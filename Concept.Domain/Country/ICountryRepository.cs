namespace Concept.Domain.Country
{
	using Infrastructure.Common.Domain;
	using System.Linq;
	using System.Threading.Tasks;

	public interface ICountryRepository : IRepository<Country, string>
	{
		#region Country

		Task<Country> GetAsync(string id);
		IQueryable<Country> GetAllQuery();

		#endregion

		#region CountryLanguage

		Task<CountryLanguage> GetCountryLanguageAsync(CountryLanguageID id);
		IQueryable<CountryLanguage> GetAllCountryLanguageQuery();
		void InsertCountryLanguage(CountryLanguage countryLanguage);
		void DeleteCountryLanguage(CountryLanguage countryLanguage);

		#endregion
	}
}
