namespace Concept.Repository.EF.Conversion
{
	using ContinentDomain = Domain.Continent;
	using CountryDomain = Domain.Country;

	public static class DomainConverter
	{
		#region Continent

		public static Models.Continent ConvertToContinentEntityType(ContinentDomain.Continent domain)
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

		#endregion

		#region Country

		public static Models.Country ConvertToCountryEntityType(CountryDomain.Country domain)
		{
			var result = default(Models.Country);

			if (domain != null)
			{
				result = new Models.Country()
				{
					countryID = domain.ID
					, countryName = domain.Name
					, continentID = domain.ContinentID
					, languageID = domain.LanguageID
					, currencyID = domain.CurrencyID
					, recStatus = domain.RecStatus
					, recCreatedBy = domain.RecCreatedBy
					, recCreatedWhen = domain.RecCreatedWhen
					, recModifyBy = domain.RecModifyBy
					, recModifyWhen = domain.RecModifyWhen
				};
			}

			return result;
		}

		#endregion
	}
}
