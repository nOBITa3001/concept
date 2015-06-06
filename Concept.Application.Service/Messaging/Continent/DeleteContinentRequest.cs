namespace Concept.Application.Service.Messaging.Continent
{
	public class DeleteContinentRequest : StringIDRequest
	{
		public ContinentPropertiesViewModel ContinentProperties { get; set; }

		public DeleteContinentRequest(string id)
			: base(id)
		{
			this.ContinentProperties = new ContinentPropertiesViewModel();
		}
	}
}