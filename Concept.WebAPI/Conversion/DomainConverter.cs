namespace Concept.WebAPI.Conversion
{
	using Domain.Continent;
	using Domain.Country;
	using System.Linq;
	using ViewModels;

	public static class DomainConverter
	{
		#region Continent

		public static IQueryable<ContinentViewModel> ConvertToContinentViewModelQuery(IQueryable<Continent> query)
		{
			var result = default(IQueryable<ContinentViewModel>);

			if (query != null)
			{
				result = query.Select(x => new ContinentViewModel()
				{
					ID = x.ID
					, Name = x.Name
					, RecStatus = x.RecStatus
				});
			}

			return result;
		}

		public static IQueryable<ContinentItemViewModel> ConvertToContinentItemViewModelQuery(IQueryable<Continent> query)
		{
			var result = default(IQueryable<ContinentItemViewModel>);

			if (query != null)
			{
				result = query.Select(x => new ContinentItemViewModel()
				{
					ID = x.ID
					, Name = x.Name
					, RecStatus = x.RecStatus
					, RecCreatedBy = x.RecCreatedBy
					, RecCreatedWhen = x.RecCreatedWhen
					, RecModifyBy = x.RecModifyBy
					, RecModifyWhen = x.RecModifyWhen
				});
			}

			return result;
		}

		#endregion

		#region Country

		public static IQueryable<CountryViewModel> ConvertToCountryViewModelQuery(IQueryable<Country> query)
		{
			var result = default(IQueryable<CountryViewModel>);

			if (query != null)
			{
				result = query.Select(x => new CountryViewModel()
				{
					ID = x.ID
					, Name = x.Name
					, ContinentID = x.ContinentID
					, ContinentName = x.Continent.Name
					, LanguageID = x.LanguageID
					, LanguageName = x.Language.Name
					, CurrencyID = x.CurrencyID
					, CurrencyName = x.Currency.Name
					, RecStatus = x.RecStatus
				});
			}

			return result;
		}

		public static IQueryable<CountryItemViewModel> ConvertToCountryItemViewModelQuery(IQueryable<Country> query)
		{
			var result = default(IQueryable<CountryItemViewModel>);

			if (query != null)
			{
				result = query.Select(x => new CountryItemViewModel()
				{
					ID = x.ID
					, Name = x.Name
					, ContinentID = x.ContinentID
					, ContinentName = x.Continent.Name
					, LanguageID = x.LanguageID
					, LanguageName = x.Language.Name
					, CurrencyID = x.CurrencyID
					, CurrencyName = x.Currency.Name
					, RecStatus = x.RecStatus
					, RecCreatedBy = x.RecCreatedBy
					, RecCreatedWhen = x.RecCreatedWhen
					, RecModifyBy = x.RecModifyBy
					, RecModifyWhen = x.RecModifyWhen
				});
			}

			return result;
		}

		#endregion

		#region CountryLanguage

		public static IQueryable<CountryLanguageViewModel> ConvertToCountryLanguageViewModelQuery(IQueryable<CountryLanguage> query)
		{
			var result = default(IQueryable<CountryLanguageViewModel>);

			if (query != null)
			{
				result = query.Select(x => new CountryLanguageViewModel()
				{
					CountryID = x.ID.CountryID
					, LanguageID = x.ID.LanguageID
					, CountryLanguageName = x.CountryName
					, CountryName = x.Country.Name
					, LanguageName = x.Language.Name
					, RecStatus = x.RecStatus
				});
			}

			return result;
		}

		public static IQueryable<CountryLanguageItemViewModel> ConvertToCountryLanguageItemViewModelQuery(IQueryable<CountryLanguage> query)
		{
			var result = default(IQueryable<CountryLanguageItemViewModel>);

			if (query != null)
			{
				result = query.Select(x => new CountryLanguageItemViewModel()
				{
					CountryID = x.ID.CountryID
					, LanguageID = x.ID.LanguageID
					, CountryLanguageName = x.CountryName
					, CountryName = x.Country.Name
					, LanguageName = x.Language.Name
					, RecStatus = x.RecStatus
					, RecCreatedBy = x.RecCreatedBy
					, RecCreatedWhen = x.RecCreatedWhen
					, RecModifyBy = x.RecModifyBy
					, RecModifyWhen = x.RecModifyWhen
				});
			}

			return result;
		}

		#endregion
	}
}