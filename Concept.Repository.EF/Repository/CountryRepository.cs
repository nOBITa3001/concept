namespace Concept.Repository.EF.Repository
{
	using Conversion;
	using Domain.Country;
	using Enum;
	using Infrastructure.Common.Domain;
	using Infrastructure.Common.UnitOfWork;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Core;
	using System.Linq;
	using System.Threading.Tasks;

	public class CountryRepository : Repository<Country, string>, ICountryRepository
	{
		#region Constructors

		public CountryRepository(IUnitOfWork unitOfWork, IObjectContextFactory objectContextFactory)
			: base(unitOfWork, objectContextFactory)
		{ }

		#endregion

		#region Implementations

		public override Country Get(string id)
		{
			var result = default(Country);

			var country = (from x in this.DbContext.Countries
							.Include(x => x.continent)
							.Include(x => x.language)
							.Include(x => x.currency)
							where x.countryID.Equals(id, StringComparison.InvariantCultureIgnoreCase)
							select x).SingleOrDefault();
			if (country != null)
			{
				result = EntityConverter.ConvertToCountryDomainType(country);
			}

			return result;
		}

		public async Task<Country> GetAsync(string id)
		{
			var result = default(Country);

			var country = await (from x in this.DbContext.Countries
									.Include(x => x.continent)
									.Include(x => x.language)
									.Include(x => x.currency)
									where x.countryID.Equals(id, StringComparison.InvariantCultureIgnoreCase)
									select x).SingleOrDefaultAsync();
			if (country != null)
			{
				result = EntityConverter.ConvertToCountryDomainType(country);
			}

			return result;
		}

		public IEnumerable<Country> GetAll()
		{
			var result = default(IEnumerable<Country>);

			var countries = (from x in this.DbContext.Countries
								.Include(x => x.continent)
								.Include(x => x.language)
								.Include(x => x.currency)
								select x);
			if (countries != null && countries.Any())
			{
				result = EntityConverter.ConvertToCountriesDomainType(countries);
			}

			return result;
		}

		public IQueryable<Country> GetAllQuery()
		{
			var query = (from x in this.DbContext.Countries
						 select x);

			return EntityConverter.ConvertToCountryDomainQuery(query);
		}

		public override async Task PersistInsertAsync(IAggregateRoot aggregateRoot)
		{
			if (aggregateRoot is Country)
			{
				var newCountry = (Country)aggregateRoot;

				var existingCountry = await this.GetAsync(newCountry.ID);
				if (existingCountry == null)
				{
					this.DbContext.Countries.Add(DomainConverter.ConvertToCountryEntityType(newCountry));
				}
				else
				{
					throw new ArgumentException(string.Concat(newCountry.ID, " already exists."));
				}
			}

			await this.DbContext.SaveChangesAsync();
		}

		public override async Task PersistUpdateAsync(IAggregateRoot aggregateRoot)
		{
			if (aggregateRoot is Country)
			{
				var updateCountryDomain = (Country)aggregateRoot;

				var existingCountryEntity = await this.DbContext.Countries.FindAsync(updateCountryDomain.ID);
				if (existingCountryEntity != null)
				{
					this.AssignUpdatePropertiesToCountryEntityType(ref existingCountryEntity
																	, updateCountryDomain);
				}
				else
				{
					throw new ObjectNotFoundException(updateCountryDomain.ID);
				}
			}

			await this.DbContext.SaveChangesAsync();
		}



		public override async Task PersistDeleteAsync(IAggregateRoot aggregateRoot)
		{
			if (aggregateRoot is Country)
			{
				var deleteCountryDomain = (Country)aggregateRoot;

				var existingCountryEntity = await this.DbContext.Countries.FindAsync(deleteCountryDomain.ID);
				if (existingCountryEntity != null)
				{
					this.AssignDeletePropertiesToCountryEntityType(ref existingCountryEntity
																	, deleteCountryDomain);
				}
				else
				{
					throw new ObjectNotFoundException(deleteCountryDomain.ID);
				}
			}

			await this.DbContext.SaveChangesAsync();
		}

		#endregion

		#region Methods

		private void AssignUpdatePropertiesToCountryEntityType(ref Models.Country entityToBeUpdate
																, Country updateCountryDomain)
		{
			if (entityToBeUpdate != null && updateCountryDomain != null)
			{
				entityToBeUpdate.countryName = updateCountryDomain.Name;
				entityToBeUpdate.continentID = updateCountryDomain.ContinentID;
				entityToBeUpdate.languageID = updateCountryDomain.LanguageID;
				entityToBeUpdate.currencyID = updateCountryDomain.CurrencyID;
				entityToBeUpdate.recStatus = updateCountryDomain.RecStatus;
				entityToBeUpdate.recModifyBy = updateCountryDomain.RecModifyBy;
				entityToBeUpdate.recModifyWhen = updateCountryDomain.RecModifyWhen;
			}
		}

		private void AssignDeletePropertiesToCountryEntityType(ref Models.Country entityToBeUpdate
																, Country deleteCountryDomain)
		{
			if (entityToBeUpdate != null && deleteCountryDomain != null)
			{
				entityToBeUpdate.recStatus = (int)RecordStatus.Delete;
				entityToBeUpdate.recModifyBy = deleteCountryDomain.RecModifyBy;
				entityToBeUpdate.recModifyWhen = deleteCountryDomain.RecModifyWhen;
			}
		}

		#endregion
	}
}
