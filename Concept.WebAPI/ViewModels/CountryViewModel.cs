namespace Concept.WebAPI.ViewModels
{
	using System.ComponentModel.DataAnnotations;

	public class CountryViewModel
	{
		[Required]
		public string ID { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string ContinentID { get; set; }
		public string ContinentName { get; set; }
		[Required]
		public string LanguageID { get; set; }
		public string LanguageName { get; set; }
		[Required]
		public string CurrencyID { get; set; }
		public string CurrencyName { get; set; }
		[Required]
		public int RecStatus { get; set; }
	}
}