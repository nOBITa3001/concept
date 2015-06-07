namespace Concept.Application.Service.Interface
{
	using Messaging.Country;
	using System.Threading.Tasks;

	public interface ICountryService
	{
		GetCountryResponse Get(GetCountryRequest getCountryRequest);
		Task<GetCountryResponse> GetAsync(GetCountryRequest getCountryRequest);
		GetCountriesResponse GetAll();
		GetCountriesQueryResponse GetAllQuery();
		Task<SaveCountryResponse> InsertAsync(InsertCountryRequest insertCountryRequest);
		Task<SaveCountryResponse> UpdateAsync(UpdateCountryRequest updateCountryRequest);
		Task<DeleteCountryResponse> DeleteAsync(DeleteCountryRequest updateCountryRequest);
	}
}
