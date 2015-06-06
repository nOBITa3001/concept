namespace Concept.Repository.EF.Repository
{
	using Enum;
	using Conversion;
	using Domain.Continent;
	using Infrastructure.Common.Domain;
	using Infrastructure.Common.UnitOfWork;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Core;
	using System.Linq;
	using System.Threading.Tasks;

	public class ContinentRepository : Repository<Continent, string>, IContinentRepository
	{
		#region Constructors

		public ContinentRepository(IUnitOfWork unitOfWork, IObjectContextFactory objectContextFactory)
			: base(unitOfWork, objectContextFactory)
		{ }

		#endregion

		#region Implementations

		public override Continent Get(string id)
		{
			var result = default(Continent);

			var continent = (from x in this.DbContext.Continents
								where x.continentID.Equals(id, StringComparison.InvariantCultureIgnoreCase)
								select x).SingleOrDefault();
			if (continent != null)
			{
				result = EntityConverter.ConvertToContinentDomainType(continent);
			}

			return result;
		}

		public async Task<Continent> GetAsync(string id)
		{
			var result = default(Continent);

			var continent = await (from x in this.DbContext.Continents
									where x.continentID.Equals(id, StringComparison.InvariantCultureIgnoreCase)
									select x).SingleOrDefaultAsync();
			if (continent != null)
			{
				result = EntityConverter.ConvertToContinentDomainType(continent);
			}

			return result;
		}

		public IEnumerable<Continent> GetAll()
		{
			var result = default(IEnumerable<Continent>);

			var continents = (from x in this.DbContext.Continents
								select x);
			if (continents != null && continents.Any())
			{
				result = EntityConverter.ConvertToContinentDomainTypes(continents);
			}

			return result;
		}

		public IQueryable<Continent> GetAllQuery()
		{
			var query = (from x in this.DbContext.Continents
							select x);

			return EntityConverter.ConvertToContinentDomainQuery(query);
		}

		public override async Task PersistInsertAsync(IAggregateRoot aggregateRoot)
		{
			if (aggregateRoot is Continent)
			{
				var newContinent = (Continent)aggregateRoot;

				var existingContinent = await this.GetAsync(newContinent.ID);
				if (existingContinent == null)
				{
					this.DbContext.Continents.Add(DomainConverter.ConvertToContinentEntityType(newContinent));
				}
				else
				{
					throw new ArgumentException(string.Concat(newContinent.ID, " already exists."));
				}
			}

			await this.DbContext.SaveChangesAsync();
		}

		public override async Task PersistUpdateAsync(IAggregateRoot aggregateRoot)
		{
			if (aggregateRoot is Continent)
			{
				var updateContinentDomain = (Continent)aggregateRoot;

				var existingContinentEntity = await this.DbContext.Continents.FindAsync(updateContinentDomain.ID);
				if (existingContinentEntity != null)
				{
					this.AssignUpdatePropertiesToContinentEntityType(ref existingContinentEntity
																		, updateContinentDomain);
				}
				else
				{
					throw new ObjectNotFoundException(updateContinentDomain.ID);
				}
			}

			await this.DbContext.SaveChangesAsync();
		}

		

		public override async Task PersistDeleteAsync(IAggregateRoot aggregateRoot)
		{
			if (aggregateRoot is Continent)
			{
				var deleteContinentDomain = (Continent)aggregateRoot;

				var existingContinentEntity = await this.DbContext.Continents.FindAsync(deleteContinentDomain.ID);
				if (existingContinentEntity != null)
				{
					this.AssignDeletePropertiesToContinentEntityType(ref existingContinentEntity
																		, deleteContinentDomain);
				}
				else
				{
					throw new ObjectNotFoundException(deleteContinentDomain.ID);
				}
			}

			await this.DbContext.SaveChangesAsync();
		}

		#endregion

		#region Methods

		private void AssignUpdatePropertiesToContinentEntityType(ref Models.Continent entityToBeUpdate
																, Continent updateContinentDomain)
		{
			if (entityToBeUpdate != null && updateContinentDomain != null)
			{
				entityToBeUpdate.continentName = updateContinentDomain.Name;
				entityToBeUpdate.recStatus = updateContinentDomain.RecStatus;
				entityToBeUpdate.recModifyBy = updateContinentDomain.RecModifyBy;
				entityToBeUpdate.recModifyWhen = updateContinentDomain.RecModifyWhen;
			}
		}

		private void AssignDeletePropertiesToContinentEntityType(ref Models.Continent entityToBeUpdate
																, Continent deleteContinentDomain)
		{
			if (entityToBeUpdate != null && deleteContinentDomain != null)
			{
				entityToBeUpdate.recStatus = (int)RecordStatus.Delete;
				entityToBeUpdate.recModifyBy = deleteContinentDomain.RecModifyBy;
				entityToBeUpdate.recModifyWhen = deleteContinentDomain.RecModifyWhen;
			}
		}

		#endregion
	}
}
