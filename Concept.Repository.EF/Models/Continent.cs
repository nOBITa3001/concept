namespace Concept.Repository.EF.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class Continent
    {
		//Base fields
		[Required]
		public string continentID { get; set; }
		[Required]
		public string continentName { get; set; }
		[Required]
		public int recStatus { get; set; }
		public string recCreatedBy { get; set; }
		public DateTime recCreatedWhen { get; set; }
		public string recModifyBy { get; set; }
		public DateTime? recModifyWhen { get; set; }
    }
}

    