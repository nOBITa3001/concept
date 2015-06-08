namespace Concept.WebAPI.ViewModels
{
	using Concept.Domain.Country;
	using System.ComponentModel.DataAnnotations;

	public class CountryLanguageViewModel
	{
		[Required]
		[Key]
		public string CountryID { get; set; }
		[Required]
		[Key]
		public string LanguageID { get; set; }
		[Required]
		public string CountryLanguageName { get; set; }
		public string CountryName { get; set; }
		public string LanguageName { get; set; }
		[Required]
		public int RecStatus { get; set; }
	}
}