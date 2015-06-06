namespace Concept.Application.Service.Messaging.Continent
{
	public class InsertContinentRequest : StringIDRequest
	{
		public ContinentPropertiesViewModel ContinentProperties { get; set; }

		public InsertContinentRequest(string id)
			: base(id)
		{
			this.ContinentProperties = new ContinentPropertiesViewModel();
		}
	}
}