namespace Concept.Repository.EF.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class Currency
    {
		//Base fields
		[Required]
		public string currencyID { get; set; }
		[Required]
		public string currencyName { get; set; }
		[Required]
		public string currencySign { get; set; }
		[Required]
		public string currencySignPosition { get; set; }
		[Required]
		public decimal currencyUplift { get; set; }
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
