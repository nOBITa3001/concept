namespace Concept.Application.Service.Interface
{
	using Messaging.Continent;
	using System.Threading.Tasks;

	public interface IContinentService
	{
		GetContinentResponse Get(GetContinentRequest getContinentRequest);
		Task<GetContinentResponse> GetAsync(GetContinentRequest getContinentRequest);
		GetContinentsResponse GetAll();
		GetContinentsQueryResponse GetAllQuery();
		Task<SaveContinentResponse> InsertAsync(InsertContinentRequest insertContinentRequest);
		Task<SaveContinentResponse> UpdateAsync(UpdateContinentRequest updateContinentRequest);
		Task<DeleteContinentResponse> DeleteAsync(DeleteContinentRequest updateContinentRequest);
	}
}
