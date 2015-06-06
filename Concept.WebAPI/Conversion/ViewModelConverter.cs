namespace Concept.WebAPI.Conversion
{
	using Application.Service.Messaging.Continent;
	using ViewModels;

	public static class ViewModelConverter
	{
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
	}
}