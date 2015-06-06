[assembly: WebActivator.PostApplicationStartMethod(typeof(Concept.WebAPI.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace Concept.WebAPI.App_Start
{
	using Application.Service.Implementation;
	using Application.Service.Interface;
	using Domain.Continent;
	using Infrastructure.Common.UnitOfWork;
	using Repository.EF;
	using Repository.EF.Repository;
	using SimpleInjector;
	using SimpleInjector.Integration.WebApi;
	using System.Web;
	using System.Web.Http;

	public static class SimpleInjectorWebApiInitializer
	{
		/// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
		public static void Initialize()
		{
			// Did you know the container can diagnose your configuration? 
			// Go to: https://simpleinjector.org/diagnostics
			var container = new Container();

			InitializeContainer(container);

			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

			container.Verify();

			GlobalConfiguration.Configuration.DependencyResolver =
				new SimpleInjectorWebApiDependencyResolver(container);
		}

		private static void InitializeContainer(Container container)
		{
			container.RegisterWebApiRequest<IContinentService, ContinentService>();
			container.RegisterWebApiRequest<IContinentRepository, ContinentRepository>();

			container.Register(() =>
			{
				var items = HttpContext.Current.Items;
				var uow = (IUnitOfWork)items["UnitOfWork"];
				if (uow == null)
				{
					items["UnitOfWork"] = uow = container.GetInstance<UnitOfWork>();
				}
				return uow;
			});
			container.RegisterWebApiRequest<IObjectContextFactory, HttpAwareOrmDataContextFactory>();
		}
	}
}