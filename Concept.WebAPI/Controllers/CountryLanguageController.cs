namespace Concept.WebAPI.Controllers
{
	using Application.Service.Interface;
	using Application.Service.Messaging.Country;
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
	using System.Web.OData.Routing;
	using ViewModels;

	public class CountryLanguageController : ODataController
	{
		#region Declarations

		private readonly ICountryService countryService;

		#endregion

		#region Constructors

		public CountryLanguageController(ICountryService countryService)
		{
			this.countryService = countryService;

			this.ThrowExceptionIfControllerIsInvalid();
		}

		#endregion

		#region Actions

		/// <summary>
		/// http://localhost:49708/odata/CountryLanguage/
		/// http://localhost:49708/odata/CountryLanguage?$top=5&$skip=2
		/// http://localhost:49708/odata/CountryLanguage?$filter=RecStatus gt 0
		/// http://localhost:49708/odata/CountryLanguage?$orderby=CountryID
		/// http://localhost:49708/odata/CountryLanguage?countryId=nl
		/// http://localhost:49708/odata/CountryLanguage?languageId=nl-nl
		/// </summary>
		[EnableQuery(PageSize = 10
					, AllowedQueryOptions = AllowedQueryOptions.Skip
											| AllowedQueryOptions.Top
											| AllowedQueryOptions.OrderBy
											| AllowedQueryOptions.Filter
					, AllowedOrderByProperties = "CountryID, LanguageID")]
		[HttpGet]
		public IQueryable<CountryLanguageViewModel> Get(ODataQueryOptions opts, string countryId = ""
												, string languageId = "", bool displayAll = false)
		{
			var result = default(IQueryable<CountryLanguageViewModel>);

			var response = this.countryService.GetAllCountryLanguageQuery();
			if (response.Exception != null)
			{
				throw new Exception(response.Exception.Message);
			}
			else
			{
				result = DomainConverter.ConvertToCountryLanguageViewModelQuery(response.Query);
				this.ApplyCondition(ref result, countryId, languageId);
				this.ApplyFilteringAndSorting(ref result, opts, displayAll);
			}

			return result;
		}

		/// <summary>
		/// http://localhost:49708/odata/CountryLanguage(CountryID='nl',LanguageID='nl-nl')
		/// http://localhost:49708/odata/CountryLanguage(CountryID='nl',LanguageID='ar-sa')
		/// http://localhost:49708/odata/CountryLanguage(CountryID='nl',LanguageID='ca-es')
		/// </summary>
		[EnableQuery]
		[ODataRoute("CountryLanguage(CountryID={countryId},LanguageID={languageId})")]
		public SingleResult<CountryLanguageItemViewModel> Get([FromODataUri] string countryId, [FromODataUri] string languageId)
		{
			var response = this.countryService.GetAllCountryLanguageQuery();
			if (response.Exception != null)
			{
				throw new Exception(response.Exception.Message);
			}

			var query = response.Query.Where(x => x.ID.CountryID.Equals(countryId, StringComparison.InvariantCultureIgnoreCase)
													&& x.ID.LanguageID.Equals(languageId, StringComparison.InvariantCultureIgnoreCase));
			return SingleResult.Create(DomainConverter.ConvertToCountryLanguageItemViewModelQuery(query));
		}

		/// <summary>
		/// http://localhost:49708/odata/CountryLanguage/
		/// { "CountryID" : "TH", "LanguageID": "en-gb"
		/// , "CountryLanguageName": "Thailand / en-gb", "RecStatus": 1 }
		/// Headers.username = "test insert data"
		/// </summary>
		[Authentication]
		public async Task<IHttpActionResult> Post(CountryLanguageViewModel countryLanguageViewModel)
		{
			var result = default(IHttpActionResult);

			if (ModelState.IsValid)
			{
				try
				{
					var insertCountryLanguageRequest = ViewModelConverter.ConvertToInsertCountryLanguageRequestType(countryLanguageViewModel);
					insertCountryLanguageRequest.CountryLanguageProperties.UserName = this.Request.Headers.GetValues("username").First();

					var response = await this.countryService.InsertCountryLanguageAsync(insertCountryLanguageRequest);
					if (response.Exception != null)
					{
						result = InternalServerError(response.Exception);
					}
					else
					{
						result = Created(countryLanguageViewModel);
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
		/// http://localhost:49708/odata/CountryLanguage(CountryID='th',LanguageID='en-gb')
		/// Headers.username = "test insert data"
		/// </summary>
		[Authentication]
		[ODataRoute("CountryLanguage(CountryID={countryId},LanguageID={languageId})")]
		public async Task<IHttpActionResult> Delete([FromODataUri] string countryId, [FromODataUri] string languageId)
		{
			var result = default(IHttpActionResult);

			if (string.IsNullOrWhiteSpace(countryId))
			{
				result = BadRequest("Country ID must not be null, empty or whitespace.");
			}
			else if (string.IsNullOrWhiteSpace(languageId))
			{
				result = BadRequest("Language ID must not be null, empty or whitespace.");
			}
			else
			{
				try
				{
					var deleteCountryLanguageRequest = new DeleteCountryLanguageRequest(countryId, languageId);
					deleteCountryLanguageRequest.CountryLanguageProperties.UserName = this.Request.Headers.GetValues("username").First();

					var response = await this.countryService.DeleteCountryLanguageAsync(deleteCountryLanguageRequest);
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
		/// Applies condition.
		/// </summary>
		private void ApplyCondition(ref IQueryable<CountryLanguageViewModel> query, string countryId, string languageId)
		{
			// Sets where country
			if (!string.IsNullOrWhiteSpace(countryId))
			{
				query = query.Where(x => x.CountryID.Equals(countryId, StringComparison.InvariantCultureIgnoreCase));
			}

			// Sets where language
			if (!string.IsNullOrWhiteSpace(languageId))
			{
				query = query.Where(x => x.LanguageID.Equals(languageId, StringComparison.InvariantCultureIgnoreCase));
			}
		}

		/// <summary>
		/// Applies filtering and sorting by checking ODataQueryOption and disaplyAll querystring.
		/// </summary>
		private void ApplyFilteringAndSorting(ref IQueryable<CountryLanguageViewModel> query, ODataQueryOptions opts, bool displayAll)
		{
			// Sets default filtering.
			if (this.DisplayAllCountries(opts, displayAll))
			{
				query = query.Where(x => x.RecStatus > 0);
			}

			// Sets default sorting.
			if (opts.OrderBy == null)
			{
				query = query.OrderBy(x => x.CountryLanguageName);
			}
		}

		/// <summary>
		/// Determines whether display all Countryies or not by checking ODataQueryOption and disaplyAll querystring.
		/// </summary>
		private bool DisplayAllCountries(ODataQueryOptions opts, bool displayAll)
		{
			return (opts.Filter == null && !displayAll);
		}

		public void ThrowExceptionIfControllerIsInvalid()
		{
			if (this.countryService == null)
			{
				throw new ArgumentNullException("CountryService in CountryController");
			}
		}

		#endregion
	}
}
