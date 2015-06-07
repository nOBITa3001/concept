namespace Concept.Repository.EF.Conversion
{
	using System.Collections.Generic;
	using System.Linq;
	using ContinentDomain = Domain.Continent;
	using CountryDomain = Domain.Country;

	public static class EntityConverter
	{
		#region Continent

		public static ContinentDomain.Continent ConvertToContinentDomainType(Models.Continent entity)
		{
			var result = default(ContinentDomain.Continent);

			if (entity != null)
			{
				result = new ContinentDomain.Continent()
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

		public static IEnumerable<ContinentDomain.Continent> ConvertToContinentsDomainType(IEnumerable<Models.Continent> entities)
		{
			var result = default(IEnumerable<ContinentDomain.Continent>);

			if (entities != null && entities.Any())
			{
				var domains = new List<ContinentDomain.Continent>();
				foreach (var entity in entities)
				{
					domains.Add(ConvertToContinentDomainType(entity));
				}

				result = domains;
			}

			return result;
		}

		public static IQueryable<ContinentDomain.Continent> ConvertToContinentDomainQuery(IQueryable<Models.Continent> query)
		{
			var result = default(IQueryable<ContinentDomain.Continent>);

			if (query != null)
			{
				result = query.Select(x => new ContinentDomain.Continent()
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

		#endregion

		#region Country

		public static CountryDomain.Country ConvertToCountryDomainType(Models.Country entity)
		{
			var result = default(CountryDomain.Country);

			if (entity != null)
			{
				result = new CountryDomain.Country()
				{
					ID = entity.countryID
					, Name = entity.countryName
					, ContinentID = entity.continentID
					, Continent = new ContinentDomain.Continent()
					{
						ID = entity.continent.continentID
						, Name = entity.continent.continentName
						, RecStatus = entity.continent.recStatus
						, RecCreatedBy = entity.continent.recCreatedBy
						, RecCreatedWhen = entity.continent.recCreatedWhen
						, RecModifyBy = entity.continent.recModifyBy
						, RecModifyWhen = entity.continent.recModifyWhen
					}
					, LanguageID = entity.languageID
					, Language = new Domain.Language.Language()
					{
						ID = entity.language.languageID
						, Name = entity.language.languageName
						, NativeName = entity.language.languageNativeName
						, Fallback = entity.language.languageFallback
						, RecStatus = entity.language.recStatus
						, RecCreatedBy = entity.language.recCreatedBy
						, RecCreatedWhen = entity.language.recCreatedWhen
						, RecModifyBy = entity.language.recModifyBy
						, RecModifyWhen = entity.language.recModifyWhen
					}
					, CurrencyID = entity.currencyID
					, Currency = new Domain.Currency.Currency()
					{
						ID = entity.currency.currencyID
						, Name = entity.currency.currencyName
						, Sign = entity.currency.currencySign
						, SignPosition = entity.currency.currencySignPosition
						, Uplift = entity.currency.currencyUplift
						, RecStatus = entity.currency.recStatus
						, RecCreatedBy = entity.currency.recCreatedBy
						, RecCreatedWhen = entity.currency.recCreatedWhen
						, RecModifyBy = entity.currency.recModifyBy
						, RecModifyWhen = entity.currency.recModifyWhen
					}
					, RecStatus = entity.recStatus
					, RecCreatedBy = entity.recCreatedBy
					, RecCreatedWhen = entity.recCreatedWhen
					, RecModifyBy = entity.recModifyBy
					, RecModifyWhen = entity.recModifyWhen
				};
			}

			return result;
		}

		public static IEnumerable<CountryDomain.Country> ConvertToCountriesDomainType(IEnumerable<Models.Country> entities)
		{
			var result = default(IEnumerable<CountryDomain.Country>);

			if (entities != null && entities.Any())
			{
				var domains = new List<CountryDomain.Country>();
				foreach (var entity in entities)
				{
					domains.Add(ConvertToCountryDomainType(entity));
				}

				result = domains;
			}

			return result;
		}

		public static IQueryable<CountryDomain.Country> ConvertToCountryDomainQuery(IQueryable<Models.Country> query)
		{
			var result = default(IQueryable<CountryDomain.Country>);

			if (query != null)
			{
				result = query.Select(x => new CountryDomain.Country()
				{
					ID = x.countryID
					, Name = x.countryName
					, ContinentID = x.continentID
					, Continent = new ContinentDomain.Continent()
					{
						ID = x.continent.continentID
						, Name = x.continent.continentName
						, RecStatus = x.continent.recStatus
						, RecCreatedBy = x.continent.recCreatedBy
						, RecCreatedWhen = x.continent.recCreatedWhen
						, RecModifyBy = x.continent.recModifyBy
						, RecModifyWhen = x.continent.recModifyWhen
					}
					, LanguageID = x.languageID
					, Language = new Domain.Language.Language()
					{
						ID = x.language.languageID
						, Name = x.language.languageName
						, NativeName = x.language.languageNativeName
						, Fallback = x.language.languageFallback
						, RecStatus = x.language.recStatus
						, RecCreatedBy = x.language.recCreatedBy
						, RecCreatedWhen = x.language.recCreatedWhen
						, RecModifyBy = x.language.recModifyBy
						, RecModifyWhen = x.language.recModifyWhen
					}
					, CurrencyID = x.currencyID
					, Currency = new Domain.Currency.Currency()
					{
						ID = x.currency.currencyID
						, Name = x.currency.currencyName
						, Sign = x.currency.currencySign
						, SignPosition = x.currency.currencySignPosition
						, Uplift = x.currency.currencyUplift
						, RecStatus = x.currency.recStatus
						, RecCreatedBy = x.currency.recCreatedBy
						, RecCreatedWhen = x.currency.recCreatedWhen
						, RecModifyBy = x.currency.recModifyBy
						, RecModifyWhen = x.currency.recModifyWhen
					}
					, RecStatus = x.recStatus
					, RecCreatedBy = x.recCreatedBy
					, RecCreatedWhen = x.recCreatedWhen
					, RecModifyBy = x.recModifyBy
					, RecModifyWhen = x.recModifyWhen
				});
			}

			return result;
		}

		#endregion
	}
}
