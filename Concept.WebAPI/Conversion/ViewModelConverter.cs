namespace Concept.WebAPI.Conversion
{
	using Application.Service.Messaging.Continent;
	using Application.Service.Messaging.Country;
	using ViewModels;

	public static class ViewModelConverter
	{
		#region Continent

		public static InsertContinentRequest ConvertToInsertContinentRequestType(ContinentViewModel viewmodel)
		{
			var result = default(InsertContinentRequest);

			if (viewmodel != null)
			{
				result = new InsertContinentRequest(viewmodel.ID)
				{
					ContinentProperties = new ContinentPropertiesViewModel()
					{
						Name = viewmodel.Name
						, RecStatus = viewmodel.RecStatus
					}
				};
			}

			return result;
		}

		public static UpdateContinentRequest ConvertToUpdateContinentRequestType(ContinentViewModel viewmodel)
		{
			var result = default(UpdateContinentRequest);

			if (viewmodel != null)
			{
				result = new UpdateContinentRequest(viewmodel.ID)
				{
					ContinentProperties = new ContinentPropertiesViewModel()
					{
						Name = viewmodel.Name
						, RecStatus = viewmodel.RecStatus
					}
				};
			}

			return result;
		}

		#endregion

		#region Country

		public static InsertCountryRequest ConvertToInsertCountryRequestType(CountryViewModel viewmodel)
		{
			var result = default(InsertCountryRequest);

			if (viewmodel != null)
			{
				result = new InsertCountryRequest(viewmodel.ID)
				{
					CountryProperties = new CountryPropertiesViewModel()
					{
						Name = viewmodel.Name
						, ContinentID = viewmodel.ContinentID
						, LanguageID = viewmodel.LanguageID
						, CurrencyID = viewmodel.CurrencyID
						, RecStatus = viewmodel.RecStatus
					}
				};
			}

			return result;
		}

		public static UpdateCountryRequest ConvertToUpdateCountryRequestType(CountryViewModel viewmodel)
		{
			var result = default(UpdateCountryRequest);

			if (viewmodel != null)
			{
				result = new UpdateCountryRequest(viewmodel.ID)
				{
					CountryProperties = new CountryPropertiesViewModel()
					{
						Name = viewmodel.Name
						, ContinentID = viewmodel.ContinentID
						, LanguageID = viewmodel.LanguageID
						, CurrencyID = viewmodel.CurrencyID
						, RecStatus = viewmodel.RecStatus
					}
				};
			}

			return result;
		}

		#endregion
	}
}