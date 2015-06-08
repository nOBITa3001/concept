namespace Concept.Repository.EF.Conversion
{
	using Models;
	using System;
	using ContinentDomain = Domain.Continent;
	using CountryDomain = Domain.Country;

	public static class DomainConverter
	{
		#region Continent

		public static Continent ConvertToContinentEntityType(ContinentDomain.Continent domain)
		{
			var result = default(Continent);

			if (domain != null)
			{
				result = new Continent()
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

		public static Country ConvertToCountryEntityType(CountryDomain.Country domain)
		{
			var result = default(Country);

			if (domain != null)
			{
				result = new Country()
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

		#region CountryLanguage

		public static CountryLanguage ConvertToCountryLanguageEntityType(CountryDomain.CountryLanguage domain)
		{
			var result = default(CountryLanguage);

			if (domain != null)
			{
				result = new CountryLanguage()
				{
					countryID = domain.ID.CountryID
					, languageID = domain.ID.LanguageID
					, countryName = domain.CountryName
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
