
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Concept.Display
{
    public class CurrencyList
    {
		public System.String currencyID { get; set; }
		public System.String currencyName { get; set; }
		public System.String currencySign { get; set; }
		public System.String currencySignPosition { get; set; }
		public System.Decimal currencyUplift { get; set; }
		public System.Int32 recStatus { get; set; }
    }
}

    