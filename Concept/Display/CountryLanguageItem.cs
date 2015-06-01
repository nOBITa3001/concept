
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Concept.Display
{
    public class CountryLanguageItem
    {
		public System.String countryID { get; set; }
		// Display field for related table
		public System.String fkCountryName { get; set; }
		public System.String languageID { get; set; }
		// Display field for related table
		public System.String fkLanguageName { get; set; }
		public System.String countryName { get; set; }
		public System.Int32 recStatus { get; set; }
		public System.String recCreatedBy { get; set; }
		public System.DateTime recCreatedWhen { get; set; }
		public System.String recModifyBy { get; set; }
		public System.Nullable<System.DateTime> recModifyWhen { get; set; }
    }
}

    