namespace Concept.WebAPI.FilterAttribute
{
	using System.Collections.Generic;
	using System.Net;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Web.Http.Filters;
	using System.Web.Http.Results;
	using WebApi.AuthenticationFilter;

	public class Authentication : AuthenticationFilterAttribute
	{
		public async override Task OnAuthenticationAsync(HttpAuthenticationContext context
															, CancellationToken cancellationToken)
		{
			if (!await this.Authenticate(context))
			{
				context.ErrorResult = new StatusCodeResult(HttpStatusCode.Unauthorized, context.Request);
			}
		}

		private Task<bool> Authenticate(HttpAuthenticationContext context)
		{
			var result = false;

			var usernames = default(IEnumerable<string>);
			result = context.Request.Headers.TryGetValues("username", out usernames);

			return Task.FromResult<bool>(result);
		}
	}
}