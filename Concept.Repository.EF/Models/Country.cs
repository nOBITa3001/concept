namespace Concept.Repository.EF.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class Country
    {
		//Base fields
		[Required]
		public string countryID { get; set; }
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
		public string continentID { get; set; }
		[Required]
		public string languageID { get; set; }
		[Required]
		public string currencyID { get; set; }

		// Navigation property
		public Continent continent { get; set; }
		public Language language { get; set; }
		public Currency currency { get; set; }
    }
}

    