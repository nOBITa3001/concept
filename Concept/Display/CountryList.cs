
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Concept.Display
{
    public class CountryList
    {
		public System.String countryID { get; set; }
		public System.String countryName { get; set; }
		public System.String continentID { get; set; }
		// Display field for related table
		public System.String fkContinentName { get; set; }
		public System.String languageID { get; set; }
		// Display field for related table
		public System.String fkLanguageName { get; set; }
		public System.String currencyID { get; set; }
		// Display field for related table
		public System.String fkCurrencyName { get; set; }
		public System.Int32 recStatus { get; set; }
    }
}

    