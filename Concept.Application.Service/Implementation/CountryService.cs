namespace Concept.Application.Service.Implementation
{
	using Domain.Country;
	using Infrastructure.Common.UnitOfWork;
	using Interface;
	using Messaging.Country;
	using System;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class CountryService : ApplicationServiceBase, ICountryService
	{
		#region Declarations

		private readonly ICountryRepository countryRepository;

		#endregion

		#region Constructors

		public CountryService(ICountryRepository countryRepository, IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			this.countryRepository = countryRepository;

			this.ThrowExceptionIfServiceIsInvalid();
		}

		#endregion

		#region Implementations

		protected override void ThrowExceptionIfServiceIsInvalid()
		{
			if (this.countryRepository == null)
			{
				throw new ArgumentNullException("Country repository");
			}
		}

		public GetCountryResponse Get(GetCountryRequest getCountryRequest)
		{
			var result = new GetCountryResponse();

			try
			{
				result.Country = this.countryRepository.Get(getCountryRequest.ID);
			}
			catch (Exception ex)
			{
				result.Exception = ex;
			}

			return result;
		}

		public async Task<GetCountryResponse> GetAsync(GetCountryRequest getCountryRequest)
		{
			var result = new GetCountryResponse();

			try
			{
				result.Country = await this.countryRepository.GetAsync(getCountryRequest.ID);
			}
			catch (Exception ex)
			{
				result.Exception = ex;
			}

			return result;
		}

		public GetCountriesResponse GetAll()
		{
			var result = new GetCountriesResponse();

			try
			{
				result.Countries = this.countryRepository.GetAll();
			}
			catch (Exception ex)
			{
				result.Exception = ex;
			}

			return result;
		}

		public GetCountriesQueryResponse GetAllQuery()
		{
			var result = new GetCountriesQueryResponse();

			try
			{
				result.Query = this.countryRepository.GetAllQuery();
			}
			catch (Exception ex)
			{
				result.Exception = ex;
			}

			return result;
		}

		public async Task<SaveCountryResponse> InsertAsync(InsertCountryRequest insertCountryRequest)
		{
			var result = new SaveCountryResponse();

			try
			{
				var newCountry = this.AssignPropertiesToCountryDomainType(insertCountryRequest.CountryProperties);
				if (newCountry == null)
				{
					throw new ArgumentNullException("CountryProperties in CountryService");
				}
				else
				{
					this.AssignInsertPropertiesToCountryDomainType(ref newCountry, insertCountryRequest);

					this.ThrowExceptionIfCountryIsInvalid(newCountry);

					this.countryRepository.Insert(newCountry);
					await this.UnitOfWork.CommitAsync();
				}
			}
			catch (Exception ex)
			{
				result.Exception = ex;
			}

			return result;
		}

		public async Task<SaveCountryResponse> UpdateAsync(UpdateCountryRequest updateCountryRequest)
		{
			var result = new SaveCountryResponse();

			try
			{
				var updateCountry = this.AssignPropertiesToCountryDomainType(updateCountryRequest.CountryProperties);
				if (updateCountry == null)
				{
					throw new ArgumentNullException("CountryProperties in CountryService");
				}
				else
				{
					this.AssignUpdatePropertiesToCountryDomainType(ref updateCountry, updateCountryRequest);

					this.countryRepository.Update(updateCountry);
					await this.UnitOfWork.CommitAsync();
				}
			}
			catch (Exception ex)
			{
				result.Exception = ex;
			}

			return result;
		}

		public async Task<DeleteCountryResponse> DeleteAsync(DeleteCountryRequest deleteCountryRequest)
		{
			var result = new DeleteCountryResponse();

			try
			{
				var deleteCountry = this.AssignPropertiesToCountryDomainType(deleteCountryRequest.CountryProperties);
				if (deleteCountry == null)
				{
					throw new ArgumentNullException("CountryProperties in CountryService");
				}
				else
				{
					this.AssignDeletePropertiesToCountryDomainType(ref deleteCountry, deleteCountryRequest);

					this.countryRepository.Delete(deleteCountry);
					await this.UnitOfWork.CommitAsync();
				}
			}
			catch (Exception ex)
			{
				result.Exception = ex;
			}

			return result;
		}

		#endregion

		#region Methods

		private void ThrowExceptionIfCountryIsInvalid(Country country)
		{
			var brokenRules = country.GetBrokenRules();
			if (brokenRules != null && brokenRules.Any())
			{
				var brokenRulesBuilder = new StringBuilder();
				brokenRulesBuilder.AppendLine("There were problems saving the country object:");
				foreach (var brokenRule in brokenRules)
				{
					brokenRulesBuilder.AppendLine(brokenRule.RuleDescription);
				}

				throw new Exception(brokenRulesBuilder.ToString());
			}
		}

		private Country AssignPropertiesToCountryDomainType(CountryPropertiesViewModel properties)
		{
			var result = default(Country);

			if (properties != null)
			{
				result = new Country()
				{
					Name = properties.Name
					, ContinentID = properties.ContinentID
					, LanguageID = properties.LanguageID
					, CurrencyID = properties.CurrencyID
					, RecStatus = properties.RecStatus
				};
			}

			return result;
		}

		private void AssignInsertPropertiesToCountryDomainType(ref Country domainToBeUpdate
																, InsertCountryRequest insertCountryRequest)
		{
			if (domainToBeUpdate != null && insertCountryRequest != null)
			{
				domainToBeUpdate.ID = insertCountryRequest.ID;
				domainToBeUpdate.RecCreatedBy = insertCountryRequest.CountryProperties.UserName;
				domainToBeUpdate.RecCreatedWhen = DateTime.Now;
			}
		}

		private void AssignUpdatePropertiesToCountryDomainType(ref Country domainToBeUpdate
																, UpdateCountryRequest updateCountryRequest)
		{
			if (domainToBeUpdate != null && updateCountryRequest != null)
			{
				domainToBeUpdate.ID = updateCountryRequest.ID;
				domainToBeUpdate.RecModifyBy = updateCountryRequest.CountryProperties.UserName;
				domainToBeUpdate.RecModifyWhen = DateTime.Now;
			}
		}

		private void AssignDeletePropertiesToCountryDomainType(ref Country domainToBeUpdate
																, DeleteCountryRequest deleteCountryRequest)
		{
			if (domainToBeUpdate != null && deleteCountryRequest != null)
			{
				domainToBeUpdate.ID = deleteCountryRequest.ID;
				domainToBeUpdate.RecModifyBy = deleteCountryRequest.CountryProperties.UserName;
				domainToBeUpdate.RecModifyWhen = DateTime.Now;
			}
		}

		#endregion
	}
}
