namespace Concept.WebAPI.Conversion
{
	using Domain.Continent;
	using System.Linq;
	using ViewModels;

	public static class DomainConverter
	{
		public static ContinentViewModel ConvertToContinentViewModelType(Continent domain)
		{
			var result = default(ContinentViewModel);

			if (domain != null)
			{
				result = new ContinentViewModel()
				{
					ID = domain.ID
					, Name = domain.Name
					, RecStatus = domain.RecStatus
				};
			}

			return result;
		}

		public static ContinentItemViewModel ConvertToContinentItemViewModelType(Continent domain)
		{
			var result = default(ContinentItemViewModel);

			if (domain != null)
			{
				result = new ContinentItemViewModel()
				{
					ID = domain.ID
					, Name = domain.Name
					, RecStatus = domain.RecStatus
					, RecCreatedBy = domain.RecCreatedBy
					, RecCreatedWhen = domain.RecCreatedWhen
					, RecModifyBy = domain.RecModifyBy
					, RecModifyWhen = domain.RecModifyWhen
				};
			}

			return result;
		}

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
	}
}