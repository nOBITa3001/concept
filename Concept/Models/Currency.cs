
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concept.Models
{
    public class Currency
    {
		//Base fields
		[Required]
		public System.String currencyID { get; set; }
		[Required]
		public System.String currencyName { get; set; }
		[Required]
		public System.String currencySign { get; set; }
		[Required]
		public System.String currencySignPosition { get; set; }
		[Required]
		public System.Decimal currencyUplift { get; set; }
		[Required]
		public System.Int32 recStatus { get; set; }
		[Required]
		public System.String recCreatedBy { get; set; }
		[Required]
		public System.DateTime recCreatedWhen { get; set; }
		public System.String recModifyBy { get; set; }
		public System.Nullable<System.DateTime> recModifyWhen { get; set; }
    }
}

    