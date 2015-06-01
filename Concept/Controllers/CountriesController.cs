using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Concept.Models;
using Concept.Display;
using Concept.Tools;

namespace Backend.Controllers
{
    public class CountriesController : ApiController
    {
        private ConceptContext db = new ConceptContext();

        // GET: api/Countries

        [ResponseType(typeof(CountryList))]
        [Route("api/countries")]
        [HttpGet]
        public IQueryable<CountryList> GetCountries(String ContinentID = "", string LanguageID = "", bool displayAll = false)
        {
            var country = from b in db.Countries
                          select new CountryList()
                          {
                              countryID = b.countryID,
                              countryName = b.countryName,
                              continentID = b.continentID,
                              fkContinentName = b.continent.continentName,
                              languageID = b.languageID,
                              fkLanguageName = b.language.languageName,
                              currencyID = b.currencyID,
                              fkCurrencyName = b.currency.currencyName,
                              recStatus = b.recStatus
                          };
            if (!string.IsNullOrEmpty(ContinentID))
                country = country.Where(b => b.continentID.Equals(ContinentID));
            if (!string.IsNullOrEmpty(LanguageID))
                country = country.Where(b => b.languageID.Equals(LanguageID));
            if (displayAll == false)
                country = country.Where(b => b.recStatus > 0);
            country = country.OrderBy(b => b.countryName);
            return country;
        }

        // GET: api/Countries/5
        [ResponseType(typeof(CountryItem))]
        [Route("api/countries/{countryID}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCountry(System.String countryID)
        {
            var country = await db.Countries.Select(b =>
               new CountryItem()
               {
                   countryID = b.countryID,
                   countryName = b.countryName,
                   languageID = b.languageID,
                   fkLanguageName = b.language.languageName,
                   continentID = b.continentID,
                   fkContinentName = b.continent.continentName,
                   currencyID=b.currencyID,
                   fkCurrencyName=b.currency.currencyName,
                   recStatus = b.recStatus,
                   recCreatedBy = b.recCreatedBy,
                   recCreatedWhen = b.recCreatedWhen,
                   recModifyBy = b.recModifyBy,
                   recModifyWhen = b.recModifyWhen
               }).SingleOrDefaultAsync(b => b.countryID == countryID);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        // PUT: api/Countries/5
        [ResponseType(typeof(CountryItem))]
        [Route("api/countries/{countryID}")]
        [HttpPut]
        public async Task<IHttpActionResult> PutCountry(System.String countryID, Country country)
        {
            if (countryID != country.countryID)
            {
                return BadRequest("URL country and Data country conflict!");
            }
            ValidationResult MyValidation = ValidateCountry(country, ApiAction.Update);
            if (!MyValidation.isSuccess())
            {
                return BadRequest(MyValidation.ToJson());
            }
            String userName = Request.Headers.GetValues("username").FirstOrDefault();

            country.recModifyBy = userName;
            country.recModifyWhen = DateTime.Now;
            db.Entry(country).State = EntityState.Modified;
            db.Entry(country).Property(x => x.recCreatedWhen).IsModified = false;
            db.Entry(country).Property(x => x.recCreatedBy).IsModified = false;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(countryID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Reload the full version of the record
            db.Entry(country).Reload();
            CountryItem UpdatedItem = GetCountryItem(countryID);
            return Ok(UpdatedItem);
        }

        // POST: api/Countries
        [ResponseType(typeof(Country))]
        [Route("api/countries")]
        [HttpPost]
        public async Task<IHttpActionResult> PostCountry(Country country)
        {
            ValidationResult MyValidation = ValidateCountry(country, ApiAction.Insert);
            if (!MyValidation.isSuccess())
            {
                return BadRequest(MyValidation.ToJson());
            }
            String userName = Request.Headers.GetValues("username").FirstOrDefault();

            //// HouseKeeping
            country.recCreatedBy = userName;
            country.recCreatedWhen = DateTime.Now;

            db.Countries.Add(country);
            await db.SaveChangesAsync();
            return Ok(GetCountryItem(country.countryID));
        }

        // DELETE: api/Countries/5
        [ResponseType(typeof(CountryItem))]
        [Route("api/countries/{countryID}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCountry(System.String countryID)
        {
            String userName = "";
            try
            {
                userName = Request.Headers.GetValues("username").FirstOrDefault();
            }
            catch
            {
                return BadRequest("No userName passed through HTTP header!");
            }

            Country country = await db.Countries.FindAsync(countryID);
            if (country == null)
            {
                return NotFound();
            }

            // Housekeeping
            country.recStatus = -1;
            country.recModifyBy = userName;
            country.recModifyWhen = DateTime.Now;
            db.Entry(country).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(countryID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Reload the full version of the record
            db.Entry(country).Reload();
            CountryItem UpdatedItem = GetCountryItem(countryID);
            return Ok(UpdatedItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountryExists(System.String countryID)
        {
            return db.Countries.Count(e => e.countryID == countryID) > 0;
        }

        private CountryItem GetCountryItem(System.String countryID)
        {

            var country = db.Countries.Where(a => a.countryID == countryID).Include(b => b.continent).Include(b => b.currency).Include(b => b.language).SingleOrDefault();
            CountryItem MyItem = new CountryItem();

            MyItem.countryID = country.countryID;
            MyItem.countryName = country.countryName;
            MyItem.languageID = country.languageID;
            MyItem.fkLanguageName = country.language.languageName;
            MyItem.continentID = country.continentID;
            MyItem.fkContinentName = country.continent.continentName;
            MyItem.currencyID = country.currencyID;
            MyItem.fkCurrencyName = country.currency.currencyName;
            MyItem.recStatus = country.recStatus;
            MyItem.recCreatedBy = country.recCreatedBy;
            MyItem.recCreatedWhen = country.recCreatedWhen;
            MyItem.recModifyBy = country.recModifyBy;
            MyItem.recModifyWhen = country.recModifyWhen;

            return (MyItem);
        }

        private ValidationResult ValidateCountry(Country country, ApiAction action)
        {
            ValidationResult thisResult = new ValidationResult();

            try
            {
                var userName = Request.Headers.GetValues("username").FirstOrDefault();
            }
            catch
            {
                thisResult.Add(1, "No username detected in http headers.");
                return thisResult;
            }

            bool Exists = CountryExists(country.countryID);
            if (Exists && action == ApiAction.Insert)
            {
                thisResult.Add(2, "Country with ID " + country.countryID.ToString() + " alreday exists.");
                return thisResult;
            }
            if (!Exists && action == ApiAction.Update)
            {
                thisResult.Add(2, "Country with ID " + country.countryID.ToString() + " does not exists.");
                return thisResult;
            }

            Continent FKcontinent = db.Continents.Find(country.continentID);
            if (FKcontinent == null)
            {
                thisResult.Add(3, "Continent with ID " + country.continentID.ToString() + " does not exist.");
                return thisResult;
            }
            Currency FKcurrency = db.Currencies.Find(country.currencyID);
            if (FKcurrency == null)
            {
                thisResult.Add(4, "Currency with ID " + country.currencyID.ToString() + "does not exist.");
                return thisResult;
            }
            Language FKlanguage = db.Languages.Find(country.languageID);
            if (FKlanguage == null)
            {
                thisResult.Add(5, "Language with ID " + country.languageID.ToString() + " does not exist.");
                return thisResult;
            }
            return thisResult;
        }
    }
}