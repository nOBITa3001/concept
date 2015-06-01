
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concept.Models
{
    public class CountryLanguage
    {
		//Base fields
		[Required]
		public System.String countryName { get; set; }
		[Required]
		public System.Int32 recStatus { get; set; }
		public System.String recCreatedBy { get; set; }
		public System.DateTime recCreatedWhen { get; set; }
		public System.String recModifyBy { get; set; }
		public System.Nullable<System.DateTime> recModifyWhen { get; set; }

		//Foreign key fields
		[Required]
		[Key]
		[Column(Order = 10)]
		public System.String countryID { get; set; }
		[Required]
		[Key]
		[Column(Order = 20)]
		public System.String languageID { get; set; }

		// Navigation property
		public Country country { get; set; }
		public Language language { get; set; }
    }
}

    