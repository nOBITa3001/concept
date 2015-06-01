
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concept.Models
{
    public class Country
    {
		//Base fields
		[Required]
		public System.String countryID { get; set; }
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
		public System.String continentID { get; set; }
		[Required]
		public System.String languageID { get; set; }
		[Required]
		public System.String currencyID { get; set; }

		// Navigation property
		public Continent continent { get; set; }
		public Language language { get; set; }
		public Currency currency { get; set; }
    }
}

    