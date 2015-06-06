namespace Concept.WebAPI.Controllers
{
	using Application.Service.Interface;
	using Application.Service.Messaging.Continent;
	using Conversion;
	using FilterAttribute;
	using System;
	using System.Data;
	using System.Data.Entity.Core;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Web.Http;
	using System.Web.OData;
	using System.Web.OData.Query;
	using ViewModels;

	public class ContinentController : ODataController
	{
		#region Declarations

		private readonly IContinentService continentService;

		#endregion

		#region Constructors

		public ContinentController(IContinentService continentService)
		{
			this.continentService = continentService;

			this.ThrowExceptionIfControllerIsInvalid();
		}

		#endregion

		#region Actions

		/// <summary>
		/// http://localhost:49708/odata/Continent/
		/// http://localhost:49708/odata/Continent?$top=5&$skip=2
		/// http://localhost:49708/odata/Continent?$filter=RecStatus gt 0
		/// http://localhost:49708/odata/Continent?$orderby=Name
		/// </summary>
		[EnableQuery(PageSize = 10
					, AllowedQueryOptions = AllowedQueryOptions.Skip
											| AllowedQueryOptions.Top
											| AllowedQueryOptions.OrderBy
											| AllowedQueryOptions.Filter
					, AllowedOrderByProperties = "ID, Name")]
		[HttpGet]
		public IQueryable<ContinentViewModel> Get(ODataQueryOptions opts, bool displayAll = false)
		{
			var result = default(IQueryable<ContinentViewModel>);

			var response = this.continentService.GetAllQuery();
			if (response.Exception != null)
			{
				throw new Exception(response.Exception.Message);
			}
			else
			{
				result = DomainConverter.ConvertToContinentViewModelQuery(response.Query);
				this.ApplyFilteringAndSorting(ref result, opts, displayAll);
			}

			return result;
		}


		/// <summary>
		/// http://localhost:49708/odata/Continent('AS')
		/// http://localhost:49708/odata/Continent('EU')
		/// http://localhost:49708/odata/Continent('NA')
		/// </summary>
		[EnableQuery]
		public SingleResult<ContinentItemViewModel> Get([FromODataUri] string key)
		{
			var response = this.continentService.GetAllQuery();
			if (response.Exception != null)
			{
				throw new Exception(response.Exception.Message);
			}

			var query = response.Query.Where(x => x.ID.Equals(key, StringComparison.InvariantCultureIgnoreCase));
			return SingleResult.Create(DomainConverter.ConvertToContinentItemViewModelQuery(query));
		}

		/// <summary>
		/// http://localhost:49708/odata/Continent/
		/// { "ID" : "XX", "Name": "Ethan", "RecStatus": 1 }
		/// Headers.username = "test insert data"
		/// </summary>
		[Authentication]
		public async Task<IHttpActionResult> Post(ContinentViewModel continentViewModel)
		{
			var result = default(IHttpActionResult);

			if (ModelState.IsValid)
			{
				try
				{
					var insertContinentRequest = ViewModelConverter.ConvertToInsertContinentRequestType(continentViewModel);
					// If a request passed Authentication, there is a username in Header.
					insertContinentRequest.ContinentProperties.UserName = this.Request.Headers.GetValues("username").First();

					var response = await this.continentService.InsertAsync(insertContinentRequest);
					if (response.Exception != null)
					{
						result = InternalServerError(response.Exception);
					}
					else
					{
						result = Created(continentViewModel);
					}
				}
				catch (ArgumentException)
				{
					result = Conflict();
				}
				catch (Exception ex)
				{
					result = InternalServerError(ex);
				}
			}
			else
			{
				result = BadRequest(ModelState);
			}

			return result;
		}

		/// <summary>
		/// http://localhost:49708/odata/Continent('XX')
		/// { "ID" : "XX", "Name": "EthanUpdate", "RecStatus": 1 }
		/// Headers.username = "test insert data"
		/// </summary>
		[Authentication]
		public async Task<IHttpActionResult> Put([FromODataUri] string key, ContinentViewModel continentViewModel)
		{
			var result = default(IHttpActionResult);

			if (!ModelState.IsValid)
			{
				result = BadRequest(ModelState);
			}
			else if (!key.Equals(continentViewModel.ID, StringComparison.InvariantCultureIgnoreCase))
			{
				result = BadRequest("URL continent and Data continent conflict!");
			}
			else
			{
				try
				{
					var updateContinentRequest = ViewModelConverter.ConvertToUpdateContinentRequestType(continentViewModel);
					// If a request passed Authentication, there is a username in Header.
					updateContinentRequest.ContinentProperties.UserName = this.Request.Headers.GetValues("username").First();

					var response = await this.continentService.UpdateAsync(updateContinentRequest);
					if (response.Exception != null)
					{
						result = InternalServerError(response.Exception);
					}
					else
					{
						result = Updated(updateContinentRequest);
					}
				}
				catch (ObjectNotFoundException)
				{
					return NotFound();
				}
				catch (Exception ex)
				{
					result = InternalServerError(ex);
				}
			}

			return result;
		}

		/// <summary>
		/// http://localhost:49708/odata/Continent('XX')
		/// Headers.username = "test insert data"
		/// </summary>
		[Authentication]
		public async Task<IHttpActionResult> Delete([FromODataUri] string key)
		{
			var result = default(IHttpActionResult);

			if (string.IsNullOrWhiteSpace(key))
			{
				result = BadRequest("ID must not be null, empty or whitespace.");
			}
			else
			{
				try
				{
					var deleteContinentRequest = new DeleteContinentRequest(key);
					// If a request passed Authentication, there is a username in Header.
					deleteContinentRequest.ContinentProperties.UserName = this.Request.Headers.GetValues("username").First();

					var response = await this.continentService.DeleteAsync(deleteContinentRequest);
					if (response.Exception != null)
					{
						result = InternalServerError(response.Exception);
					}
					else
					{
						result = Ok();
					}
				}
				catch (ObjectNotFoundException)
				{
					return NotFound();
				}
				catch (Exception ex)
				{
					result = InternalServerError(ex);
				}
			}

			return result;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Applies filtering and sorting by checking ODataQueryOption and disaplyAll querystring.
		/// </summary>
		private IQueryable<ContinentViewModel> ApplyFilteringAndSorting(ref IQueryable<ContinentViewModel> query, ODataQueryOptions opts, bool displayAll)
		{
			// Sets default filtering.
			if (this.DisplayAllContinents(opts, displayAll))
			{
				query = query.Where(x => x.RecStatus > 0);
			}

			// Sets default sorting.
			if (opts.OrderBy == null)
			{
				query = query.OrderBy(x => x.Name);
			}

			return query;
		}

		/// <summary>
		/// Determines whether display all Continents or not by checking ODataQueryOption and disaplyAll querystring.
		/// </summary>
		private bool DisplayAllContinents(ODataQueryOptions opts, bool displayAll)
		{
			return (opts.Filter == null && !displayAll);
		}

		public void ThrowExceptionIfControllerIsInvalid()
		{
			if (this.continentService == null)
			{
				throw new ArgumentNullException("ContinentService in ContinentController");
			}
		}

		#endregion
	}
}
