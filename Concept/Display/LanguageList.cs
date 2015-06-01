
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Concept.Display
{
    public class LanguageList
    {
		public System.String languageID { get; set; }
		public System.String languageName { get; set; }
		public System.String languageNativeName { get; set; }
		public System.String languageFallback { get; set; }
		public System.Int32 recStatus { get; set; }
    }
}

    