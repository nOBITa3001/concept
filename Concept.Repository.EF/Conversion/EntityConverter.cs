namespace Concept.Repository.EF.Conversion
{
	using System.Collections.Generic;
	using System.Linq;
	using Domains = Domain.Continent;

	public static class EntityConverter
	{
		public static Domains.Continent ConvertToContinentDomainType(Models.Continent entity)
		{
			var result = default(Domains.Continent);

			if (entity != null)
			{
				result = new Domains.Continent()
				{
					ID = entity.continentID
					, Name = entity.continentName
					, RecStatus = entity.recStatus
					, RecCreatedBy = entity.recCreatedBy
					, RecCreatedWhen = entity.recCreatedWhen
					, RecModifyBy = entity.recModifyBy
					, RecModifyWhen = entity.recModifyWhen
				};
			}

			return result;
		}

		public static IEnumerable<Domains.Continent> ConvertToContinentDomainTypes(IEnumerable<Models.Continent> entities)
		{
			var result = default(IEnumerable<Domains.Continent>);

			if (entities != null && entities.Any())
			{
				var domains = new List<Domains.Continent>();
				foreach (var entity in entities)
				{
					domains.Add(ConvertToContinentDomainType(entity));
				}

				result = domains;
			}

			return result;
		}

		public static IQueryable<Domains.Continent> ConvertToContinentDomainQuery(IQueryable<Models.Continent> query)
		{
			var result = default(IQueryable<Domains.Continent>);

			if (query != null)
			{
				result = query.Select(x => new Domains.Continent()
				{
					ID = x.continentID
					, Name = x.continentName
					, RecStatus = x.recStatus
					, RecCreatedBy = x.recCreatedBy
					, RecCreatedWhen = x.recCreatedWhen
					, RecModifyBy = x.recCreatedBy
					, RecModifyWhen = x.recModifyWhen
				});
			}

			return result;
		}
	}
}
