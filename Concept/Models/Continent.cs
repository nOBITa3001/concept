
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concept.Models
{
    public class Continent
    {
		//Base fields
		[Required]
		public System.String continentID { get; set; }
		[Required]
		public System.String continentName { get; set; }
		[Required]
		public System.Int32 recStatus { get; set; }
		public System.String recCreatedBy { get; set; }
		public System.DateTime recCreatedWhen { get; set; }
		public System.String recModifyBy { get; set; }
		public System.Nullable<System.DateTime> recModifyWhen { get; set; }
    }
}

    