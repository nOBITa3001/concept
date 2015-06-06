namespace Concept.Application.Service.Messaging.Continent
{
	public class UpdateContinentRequest : StringIDRequest
	{
		public ContinentPropertiesViewModel ContinentProperties { get; set; }

		public UpdateContinentRequest(string id)
			: base(id)
		{
			this.ContinentProperties = new ContinentPropertiesViewModel();
		}
	}
}