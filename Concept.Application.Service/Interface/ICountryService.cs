namespace Concept.Application.Service.Interface
{
	using Messaging.Country;
	using System.Threading.Tasks;

	public interface ICountryService
	{
		#region Country

		GetCountryResponse Get(GetCountryRequest getCountryRequest);
		Task<GetCountryResponse> GetAsync(GetCountryRequest getCountryRequest);
		GetCountriesResponse GetAll();
		GetCountriesQueryResponse GetAllQuery();
		Task<SaveCountryResponse> InsertAsync(InsertCountryRequest insertCountryRequest);
		Task<SaveCountryResponse> UpdateAsync(UpdateCountryRequest updateCountryRequest);
		Task<DeleteCountryResponse> DeleteAsync(DeleteCountryRequest deleteCountryRequest);

		#endregion

		#region CountryLanguage

		GetCountryLanguagesQueryResponse GetAllCountryLanguageQuery();
		Task<SaveCountryLanguageResponse> InsertCountryLanguageAsync(InsertCountryLanguageRequest insertCountryRequest);
		Task<DeleteCountryLanguageResponse> DeleteCountryLanguageAsync(DeleteCountryLanguageRequest deleteCountryRequest);
		
		#endregion
	}
}
