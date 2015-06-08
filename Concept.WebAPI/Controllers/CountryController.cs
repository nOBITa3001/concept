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
	using ViewModels;

	public class CountryController : ODataController
	{
		#region Declarations

		private readonly ICountryService countryService;

		#endregion

		#region Constructors

		public CountryController(ICountryService countryService)
		{
			this.countryService = countryService;

			this.ThrowExceptionIfControllerIsInvalid();
		}

		#endregion

		#region Actions

		/// <summary>
		/// http://localhost:49708/odata/Country/
		/// http://localhost:49708/odata/Country?$top=5&$skip=2
		/// http://localhost:49708/odata/Country?$filter=RecStatus gt 0
		/// http://localhost:49708/odata/Country?$orderby=Name
		/// http://localhost:49708/odata/Country?continentId=AS
		/// http://localhost:49708/odata/Country?languageId=nl-nl
		/// </summary>
		[EnableQuery(PageSize = 10
					, AllowedQueryOptions = AllowedQueryOptions.Skip
											| AllowedQueryOptions.Top
											| AllowedQueryOptions.OrderBy
											| AllowedQueryOptions.Filter
					, AllowedOrderByProperties = "ID, Name")]
		[HttpGet]
		public IQueryable<CountryViewModel> Get(ODataQueryOptions opts, string continentId = ""
												, string languageId = "", bool displayAll = false)
		{
			var result = default(IQueryable<CountryViewModel>);

			var response = this.countryService.GetAllQuery();
			if (response.Exception != null)
			{
				throw new Exception(response.Exception.Message);
			}
			else
			{
				result = DomainConverter.ConvertToCountryViewModelQuery(response.Query);
				this.ApplyCondition(ref result, continentId, languageId);
				this.ApplyFilteringAndSorting(ref result, opts, displayAll);
			}

			return result;
		}

		/// <summary>
		/// http://localhost:49708/odata/Country('GB')
		/// http://localhost:49708/odata/Country('US')
		/// http://localhost:49708/odata/Country('TH')
		/// </summary>
		[EnableQuery]
		public SingleResult<CountryItemViewModel> Get([FromODataUri] string key)
		{
			var response = this.countryService.GetAllQuery();
			if (response.Exception != null)
			{
				throw new Exception(response.Exception.Message);
			}

			var query = response.Query.Where(x => x.ID.Equals(key, StringComparison.InvariantCultureIgnoreCase));
			return SingleResult.Create(DomainConverter.ConvertToCountryItemViewModelQuery(query));
		}

		/// <summary>
		/// http://localhost:49708/odata/Country/
		/// { "ID" : "X4", "Name": "Netherlands"
		/// , "ContinentID": "EU", "LanguageID": "nl-nl"
		/// , "CurrencyID": "GBP", "RecStatus": 1 }
		/// Headers.username = "test insert data"
		/// </summary>
		[Authentication]
		public async Task<IHttpActionResult> Post(CountryViewModel countryViewModel)
		{
			var result = default(IHttpActionResult);

			if (ModelState.IsValid)
			{
				try
				{
					var insertCountryRequest = ViewModelConverter.ConvertToInsertCountryRequestType(countryViewModel);
					insertCountryRequest.CountryProperties.UserName = this.Request.Headers.GetValues("username").First();

					var response = await this.countryService.InsertAsync(insertCountryRequest);
					if (response.Exception != null)
					{
						result = InternalServerError(response.Exception);
					}
					else
					{
						result = Created(countryViewModel);
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
		/// http://localhost:49708/odata/Country('X4')
		/// { "ID" : "X4", "Name": "NewThailand"
		/// , "ContinentID": "AS", "LanguageID": "en-gb"
		/// , "CurrencyID": "THB", "RecStatus": 1 }
		/// Headers.username = "test insert data"
		/// </summary>
		[Authentication]
		public async Task<IHttpActionResult> Put([FromODataUri] string key, CountryViewModel countryViewModel)
		{
			var result = default(IHttpActionResult);

			if (!ModelState.IsValid)
			{
				result = BadRequest(ModelState);
			}
			else if (!key.Equals(countryViewModel.ID, StringComparison.InvariantCultureIgnoreCase))
			{
				result = BadRequest("URL country and Data country conflict!");
			}
			else
			{
				try
				{
					var updateCountryRequest = ViewModelConverter.ConvertToUpdateCountryRequestType(countryViewModel);
					updateCountryRequest.CountryProperties.UserName = this.Request.Headers.GetValues("username").First();

					var response = await this.countryService.UpdateAsync(updateCountryRequest);
					if (response.Exception != null)
					{
						result = InternalServerError(response.Exception);
					}
					else
					{
						result = Updated(updateCountryRequest);
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
		/// http://localhost:49708/odata/Country('X4')
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
					var deleteCountryRequest = new DeleteCountryRequest(key);
					deleteCountryRequest.CountryProperties.UserName = this.Request.Headers.GetValues("username").First();

					var response = await this.countryService.DeleteAsync(deleteCountryRequest);
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
		private void ApplyCondition(ref IQueryable<CountryViewModel> query, string continentId, string languageId)
		{
			// Sets where continent
			if (!string.IsNullOrWhiteSpace(continentId))
			{
				query = query.Where(x => x.ContinentID.Equals(continentId, StringComparison.InvariantCultureIgnoreCase));
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
		private void ApplyFilteringAndSorting(ref IQueryable<CountryViewModel> query, ODataQueryOptions opts, bool displayAll)
		{
			// Sets default filtering.
			if (this.DisplayAllCountries(opts, displayAll))
			{
				query = query.Where(x => x.RecStatus > 0);
			}

			// Sets default sorting.
			if (opts.OrderBy == null)
			{
				query = query.OrderBy(x => x.Name);
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
