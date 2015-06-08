namespace Concept.WebAPI
{
	using System.Web.Http;
	using System.Web.OData.Builder;
	using System.Web.OData.Extensions;
	using ViewModels;

	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			// OData routes
			ODataModelBuilder builder = new ODataConventionModelBuilder();
			builder.EntitySet<ContinentViewModel>("Continent");
			builder.EntitySet<ContinentItemViewModel>("ContinentItem");
			builder.EntitySet<CountryViewModel>("Country");
			builder.EntitySet<CountryItemViewModel>("CountryItem");
			builder.EntitySet<CountryLanguageViewModel>("CountryLanguage");
			builder.EntitySet<CountryLanguageItemViewModel>("CountryLanguageItem");
			config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

			// Setup return json instead
			var json = config.Formatters.JsonFormatter;
			json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
			config.Formatters.Remove(config.Formatters.XmlFormatter);
		}
	}
}
