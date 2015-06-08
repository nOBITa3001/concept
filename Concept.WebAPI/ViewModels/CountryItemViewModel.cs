namespace Concept.WebAPI.ViewModels
{
	using System;

	public class CountryItemViewModel : CountryViewModel
	{
		public string RecCreatedBy { get; set; }
		public DateTime RecCreatedWhen { get; set; }
		public string RecModifyBy { get; set; }
		public DateTime? RecModifyWhen { get; set; }
	}
}