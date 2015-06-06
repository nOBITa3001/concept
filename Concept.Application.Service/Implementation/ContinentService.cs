namespace Concept.Application.Service.Implementation
{
	using Domain.Continent;
	using Infrastructure.Common.UnitOfWork;
	using Interface;
	using Messaging.Continent;
	using System;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class ContinentService : ApplicationServiceBase, IContinentService
	{
		#region Declarations

		private readonly IContinentRepository continentRepository;

		#endregion

		#region Constructors

		public ContinentService(IContinentRepository continentRepository, IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			this.continentRepository = continentRepository;

			this.ThrowExceptionIfServiceIsInvalid();
		}

		#endregion

		#region Implementations

		protected override void ThrowExceptionIfServiceIsInvalid()
		{
			if (this.continentRepository == null)
			{
				throw new ArgumentNullException("Continent repository");
			}
		}

		public GetContinentResponse Get(GetContinentRequest getContinentRequest)
		{
			var result = new GetContinentResponse();

			try
			{
				result.Continent = this.continentRepository.Get(getContinentRequest.ID);
			}
			catch (Exception ex)
			{
				result.Exception = ex;
			}

			return result;
		}

		public async Task<GetContinentResponse> GetAsync(GetContinentRequest getContinentRequest)
		{
			var result = new GetContinentResponse();

			try
			{
				result.Continent = await this.continentRepository.GetAsync(getContinentRequest.ID);
			}
			catch (Exception ex)
			{
				result.Exception = ex;
			}

			return result;
		}

		public GetContinentsResponse GetAll()
		{
			var result = new GetContinentsResponse();

			try
			{
				result.Continents = this.continentRepository.GetAll();
			}
			catch (Exception ex)
			{
				result.Exception = ex;
			}

			return result;
		}

		public GetContinentsQueryResponse GetAllQuery()
		{
			var result = new GetContinentsQueryResponse();

			try
			{
				result.Query = this.continentRepository.GetAllQuery();
			}
			catch (Exception ex)
			{
				result.Exception = ex;
			}

			return result;
		}

		public async Task<SaveContinentResponse> InsertAsync(InsertContinentRequest insertContinentRequest)
		{
			var result = new SaveContinentResponse();

			try
			{
				var newContinent = this.AssignPropertiesToContinentDomainType(insertContinentRequest.ContinentProperties);
				if (newContinent == null)
				{
					throw new ArgumentNullException("ContinentProperties in ContinentService");
				}
				else
				{
					this.AssignInsertPropertiesToContinentDomainType(ref newContinent, insertContinentRequest);

					this.ThrowExceptionIfContinentIsInvalid(newContinent);

					this.continentRepository.Insert(newContinent);
					await this.UnitOfWork.CommitAsync();
				}
			}
			catch (Exception ex)
			{
				result.Exception = ex;
			}

			return result;
		}

		public async Task<SaveContinentResponse> UpdateAsync(UpdateContinentRequest updateContinentRequest)
		{
			var result = new SaveContinentResponse();

			try
			{
				var updateContinent = this.AssignPropertiesToContinentDomainType(updateContinentRequest.ContinentProperties);
				if (updateContinent == null)
				{
					throw new ArgumentNullException("ContinentProperties in ContinentService");
				}
				else
				{
					this.AssignUpdatePropertiesToContinentDomainType(ref updateContinent, updateContinentRequest);

					this.continentRepository.Update(updateContinent);
					await this.UnitOfWork.CommitAsync();
				}
			}
			catch (Exception ex)
			{
				result.Exception = ex;
			}

			return result;
		}

		public async Task<DeleteContinentResponse> DeleteAsync(DeleteContinentRequest deleteContinentRequest)
		{
			var result = new DeleteContinentResponse();

			try
			{
				var deleteContinent = this.AssignPropertiesToContinentDomainType(deleteContinentRequest.ContinentProperties);
				if (deleteContinent == null)
				{
					throw new ArgumentNullException("ContinentProperties in ContinentService");
				}
				else
				{
					this.AssignDeletePropertiesToContinentDomainType(ref deleteContinent, deleteContinentRequest);

					this.continentRepository.Delete(deleteContinent);
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

		private void ThrowExceptionIfContinentIsInvalid(Continent continent)
		{
			var brokenRules = continent.GetBrokenRules();
			if (brokenRules != null && brokenRules.Any())
			{
				var brokenRulesBuilder = new StringBuilder();
				brokenRulesBuilder.AppendLine("There were problems saving the continent object:");
				foreach (var brokenRule in brokenRules)
				{
					brokenRulesBuilder.AppendLine(brokenRule.RuleDescription);
				}

				throw new Exception(brokenRulesBuilder.ToString());
			}
		}

		private Continent AssignPropertiesToContinentDomainType(ContinentPropertiesViewModel properties)
		{
			var result = default(Continent);

			if (properties != null)
			{
				result = new Continent()
				{
					Name = properties.Name
					, RecStatus = properties.RecStatus
				};
			}

			return result;
		}

		private void AssignInsertPropertiesToContinentDomainType(ref Continent domainToBeUpdate
																, InsertContinentRequest insertContinentRequest)
		{
			if (domainToBeUpdate != null && insertContinentRequest != null)
			{
				domainToBeUpdate.ID = insertContinentRequest.ID;
				domainToBeUpdate.RecCreatedBy = insertContinentRequest.ContinentProperties.UserName;
				domainToBeUpdate.RecCreatedWhen = DateTime.Now;
			}
		}

		private void AssignUpdatePropertiesToContinentDomainType(ref Continent domainToBeUpdate
																	, UpdateContinentRequest updateContinentRequest)
		{
			if (domainToBeUpdate != null && updateContinentRequest != null)
			{
				domainToBeUpdate.ID = updateContinentRequest.ID;
				domainToBeUpdate.RecModifyBy = updateContinentRequest.ContinentProperties.UserName;
				domainToBeUpdate.RecModifyWhen = DateTime.Now;
			}
		}

		private void AssignDeletePropertiesToContinentDomainType(ref Continent domainToBeUpdate
																	, DeleteContinentRequest deleteContinentRequest)
		{
			if (domainToBeUpdate != null && deleteContinentRequest != null)
			{
				domainToBeUpdate.ID = deleteContinentRequest.ID;
				domainToBeUpdate.RecModifyBy = deleteContinentRequest.ContinentProperties.UserName;
				domainToBeUpdate.RecModifyWhen = DateTime.Now;
			}
		}

		#endregion
	}
}
