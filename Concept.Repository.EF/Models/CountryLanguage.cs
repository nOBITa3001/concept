namespace Concept.Repository.EF.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class CountryLanguage
    {
		//Base fields
		[Required]
		public string countryName { get; set; }
		[Required]
		public int recStatus { get; set; }
		public string recCreatedBy { get; set; }
		public DateTime recCreatedWhen { get; set; }
		public string recModifyBy { get; set; }
		public DateTime? recModifyWhen { get; set; }

		//Foreign key fields
		[Required]
		[Key]
		[Column(Order = 10)]
		public string countryID { get; set; }
		[Required]
		[Key]
		[Column(Order = 20)]
		public string languageID { get; set; }

		// Navigation property
		public Country country { get; set; }
		public Language language { get; set; }
    }
}

    