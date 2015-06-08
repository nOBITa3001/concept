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

		#region Country

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

		#region CountryLanguage

		public GetCountryLanguagesQueryResponse GetAllCountryLanguageQuery()
		{
			var result = new GetCountryLanguagesQueryResponse();

			try
			{
				result.Query = this.countryRepository.GetAllCountryLanguageQuery();
			}
			catch (Exception ex)
			{
				result.Exception = ex;
			}

			return result;
		}

		public async Task<SaveCountryLanguageResponse> InsertCountryLanguageAsync(InsertCountryLanguageRequest insertCountryLanguageRequest)
		{
			var result = new SaveCountryLanguageResponse();

			try
			{
				var newCountryLanguage = this.AssignPropertiesToCountryLanguageDomainType(insertCountryLanguageRequest.CountryLanguageProperties);
				if (newCountryLanguage == null)
				{
					throw new ArgumentNullException("CountryLanguageProperties in CountryService");
				}
				else
				{
					this.AssignInsertCountryLanguagePropertiesToCountryLanguageDomainType(ref newCountryLanguage, insertCountryLanguageRequest);

					this.ThrowExceptionIfCountryLanguageIsInvalid(newCountryLanguage);

					this.countryRepository.InsertCountryLanguage(newCountryLanguage);
					await this.UnitOfWork.CommitAsync();
				}
			}
			catch (Exception ex)
			{
				result.Exception = ex;
			}

			return result;
		}

		public async Task<DeleteCountryLanguageResponse> DeleteCountryLanguageAsync(DeleteCountryLanguageRequest deleteCountryLanguageRequest)
		{
			var result = new DeleteCountryLanguageResponse();

			try
			{
				var deleteCountryLanguage = this.AssignPropertiesToCountryLanguageDomainType(deleteCountryLanguageRequest.CountryLanguageProperties);
				if (deleteCountryLanguage == null)
				{
					throw new ArgumentNullException("CountryLanguageProperties in CountryService");
				}
				else
				{
					this.AssignDeleteCountryLanguagePropertiesToCountryLanguageDomainType(ref deleteCountryLanguage, deleteCountryLanguageRequest);

					this.countryRepository.DeleteCountryLanguage(deleteCountryLanguage);
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

		#endregion

		#region Methods

		#region Country

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

		#region CountryLanguage

		private void ThrowExceptionIfCountryLanguageIsInvalid(CountryLanguage countryLanguage)
		{
			var brokenRules = countryLanguage.GetBrokenRules();
			if (brokenRules != null && brokenRules.Any())
			{
				var brokenRulesBuilder = new StringBuilder();
				brokenRulesBuilder.AppendLine("There were problems saving the country language object:");
				foreach (var brokenRule in brokenRules)
				{
					brokenRulesBuilder.AppendLine(brokenRule.RuleDescription);
				}

				throw new Exception(brokenRulesBuilder.ToString());
			}
		}

		private CountryLanguage AssignPropertiesToCountryLanguageDomainType(CountryLanguagePropertiesViewModel properties)
		{
			var result = default(CountryLanguage);

			if (properties != null)
			{
				result = new CountryLanguage()
				{
					CountryName = properties.CountryName
					, RecStatus = properties.RecStatus
				};
			}

			return result;
		}

		private void AssignInsertCountryLanguagePropertiesToCountryLanguageDomainType(ref CountryLanguage domainToBeUpdate
																						, InsertCountryLanguageRequest insertCountryLanguageRequest)
		{
			if (domainToBeUpdate != null && insertCountryLanguageRequest != null)
			{
				domainToBeUpdate.ID = insertCountryLanguageRequest.ID;
				domainToBeUpdate.RecCreatedBy = insertCountryLanguageRequest.CountryLanguageProperties.UserName;
				domainToBeUpdate.RecCreatedWhen = DateTime.Now;
			}
		}

		private void AssignDeleteCountryLanguagePropertiesToCountryLanguageDomainType(ref CountryLanguage domainToBeUpdate
																						, DeleteCountryLanguageRequest deleteCountryLanguageRequest)
		{
			if (domainToBeUpdate != null && deleteCountryLanguageRequest != null)
			{
				domainToBeUpdate.ID = deleteCountryLanguageRequest.ID;
				domainToBeUpdate.RecModifyBy = deleteCountryLanguageRequest.CountryLanguageProperties.UserName;
				domainToBeUpdate.RecModifyWhen = DateTime.Now;
			}
		}

		#endregion

		#endregion
	}
}
