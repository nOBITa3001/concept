
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concept.Models
{
    public class Language
    {
		//Base fields
		[Required]
		public System.String languageID { get; set; }
		[Required]
		public System.String languageName { get; set; }
		[Required]
		public System.String languageNativeName { get; set; }
		[Required]
		public System.String languageFallback { get; set; }
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

    