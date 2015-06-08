namespace Concept.Repository.EF
{
	using Models;
	using System.Web;

	public class HttpAwareOrmDataContextFactory : IObjectContextFactory
	{
		private const string DATA_CONTEXT_KEY = "DataContext";

		public ConceptContext Create()
		{
			var objectContext = default(ConceptContext);

			if (HttpContext.Current.Items.Contains(DATA_CONTEXT_KEY))
			{
				objectContext = HttpContext.Current.Items[DATA_CONTEXT_KEY] as ConceptContext;
			}
			else
			{
				objectContext = new ConceptContext();
				this.Store(objectContext);
			}

			return objectContext;
		}

		private void Store(ConceptContext objectContext)
		{
			if (HttpContext.Current.Items.Contains(DATA_CONTEXT_KEY))
			{
				HttpContext.Current.Items[DATA_CONTEXT_KEY] = objectContext;
			}
			else
			{
				HttpContext.Current.Items.Add(DATA_CONTEXT_KEY, objectContext);
			}
		}
	}
}
