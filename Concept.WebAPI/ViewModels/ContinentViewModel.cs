namespace Concept.WebAPI.ViewModels
{
	using System.ComponentModel.DataAnnotations;

	public class ContinentViewModel
	{
		[Required]
		public string ID { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int RecStatus { get; set; }
	}
}