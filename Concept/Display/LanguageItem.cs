
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Concept.Display
{
    public class LanguageItem
    {
		public System.String languageID { get; set; }
		public System.String languageName { get; set; }
		public System.String languageNativeName { get; set; }
		public System.String languageFallback { get; set; }
		public System.Int32 recStatus { get; set; }
		public System.String recCreatedBy { get; set; }
		public System.DateTime recCreatedWhen { get; set; }
		public System.String recModifyBy { get; set; }
		public System.Nullable<System.DateTime> recModifyWhen { get; set; }
    }
}

    