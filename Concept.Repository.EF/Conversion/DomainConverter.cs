namespace Concept.Repository.EF.Conversion
{
	using Domains = Domain.Continent;

	public static class DomainConverter
	{
		public static Models.Continent ConvertToContinentEntityType(Domains.Continent domain)
		{
			var result = default(Models.Continent);

			if (domain != null)
			{
				result = new Models.Continent()
				{
					continentID = domain.ID
					, continentName = domain.Name
					, recStatus = domain.RecStatus
					, recCreatedBy = domain.RecCreatedBy
					, recCreatedWhen = domain.RecCreatedWhen
					, recModifyBy = domain.RecModifyBy
					, recModifyWhen = domain.RecModifyWhen
				};
			}

			return result;
		}
	}
}
