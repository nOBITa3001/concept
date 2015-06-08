namespace Concept.Repository.EF.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class Language
    {
		//Base fields
		[Required]
		public string languageID { get; set; }
		[Required]
		public string languageName { get; set; }
		[Required]
		public string languageNativeName { get; set; }
		[Required]
		public string languageFallback { get; set; }
		[Required]
		public int recStatus { get; set; }
		[Required]
		public string recCreatedBy { get; set; }
		[Required]
		public DateTime recCreatedWhen { get; set; }
		public string recModifyBy { get; set; }
		public DateTime? recModifyWhen { get; set; }
    }
}
