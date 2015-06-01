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
    public class CountryLanguagesController : ApiController
    {
        private ConceptContext db = new ConceptContext();

        // GET: api/CountryLanguges
        [ResponseType(typeof(CountryLanguageList))]
        [Route("api/countryLanguages")]
        [HttpGet]
        public IQueryable<CountryLanguageList> GetCountryLanguges(string CountryID = "", string LanguageID = "", bool displayAll = false)
        {
            var countrylanguage = from b in db.CountryLanguages
                                  select new CountryLanguageList()
                                  {
                                      countryID = b.countryID,
                                      fkCountryName = b.country.countryName,
                                      languageID = b.languageID,
                                      fkLanguageName = b.language.languageName,
                                      countryName = b.countryName,
                                      recStatus = b.recStatus
                                  };
            if (!string.IsNullOrEmpty(CountryID))
                countrylanguage = countrylanguage.Where(b => b.countryID.Equals(CountryID));
            if (!string.IsNullOrEmpty(LanguageID))
                countrylanguage = countrylanguage.Where(b => b.languageID.Equals(LanguageID));
            if (displayAll == false)
                countrylanguage = countrylanguage.Where(b => b.recStatus > 0);
            countrylanguage = countrylanguage.OrderBy(b => b.countryID);
            return countrylanguage;
        }

        // GET: api/Countries/5
        [ResponseType(typeof(CountryLanguageItem))]
        [Route("api/countryLanguages/{countryID}/{languageID}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCountryLanguageByCountryAndLanguage(System.String countryID, System.String languageID)
        {
            var countrylanguage = await db.CountryLanguages.Select(b =>
                new CountryLanguageItem()
                {
                    countryID = b.countryID,
                    fkCountryName = b.country.countryName,
                    languageID = b.languageID,
                    fkLanguageName = b.language.languageName,
                    countryName = b.countryName,
                    recStatus = b.recStatus,
                    recCreatedBy = b.recCreatedBy,
                    recCreatedWhen = b.recCreatedWhen,
                    recModifyBy = b.recModifyBy,
                    recModifyWhen = b.recModifyWhen
                }).SingleOrDefaultAsync(b => b.countryID == countryID && b.languageID == languageID);
            if (countrylanguage == null)
            {
                return NotFound();
            }
            return Ok(countrylanguage);
        }

        // POST: api/Countries
        [ResponseType(typeof(CountryLanguageItem))]
        [Route("api/countryLanguages")]
        [HttpPost]
        public async Task<IHttpActionResult> PostCountryLanguage(CountryLanguage countrylanguage)
        {
            ValidationResult MyValidation = ValidateCountryLanguage(countrylanguage, ApiAction.Insert);
            if (!MyValidation.isSuccess())
            {
                return BadRequest(MyValidation.ToJson());
            }

            String userName = Request.Headers.GetValues("username").FirstOrDefault();

            // HouseKeeping
            countrylanguage.recCreatedBy = userName;
            countrylanguage.recCreatedWhen = DateTime.Now;

            db.CountryLanguages.Add(countrylanguage);
            await db.SaveChangesAsync();
            return Ok(GetCountryLanguageItem(countrylanguage.countryID, countrylanguage.languageID));
        }

        // PUT: api/Countries/5
        [ResponseType(typeof(CountryLanguageItem))]
        [Route("api/countryLanguages/{countryID}/{languageID}")]
        [HttpPut]
        public async Task<IHttpActionResult> PutCountryLanguage(CountryLanguage countrylanguage, string countryID, string languageID)
        {
            ValidationResult MyValidation = ValidateCountryLanguage(countrylanguage, ApiAction.Update);
            if (countryID != countrylanguage.countryID || languageID != countrylanguage.languageID)
            {
                MyValidation.Add(6, "URL country/language and Data country/language conflict!");
            }
            if (!MyValidation.isSuccess())
            {
                return BadRequest(MyValidation.ToJson());
            }
            String userName = Request.Headers.GetValues("username").FirstOrDefault();
            countrylanguage.recModifyBy = userName;
            countrylanguage.recModifyWhen = DateTime.Now;
            db.Entry(countrylanguage).State = EntityState.Modified;
            db.Entry(countrylanguage).Property(x => x.recCreatedWhen).IsModified = false;
            db.Entry(countrylanguage).Property(x => x.recCreatedBy).IsModified = false;

            try
            {
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

            // Reload the full version of the record
            db.Entry(countrylanguage).Reload();
            CountryLanguageItem UpdatedItem = GetCountryLanguageItem(countrylanguage.countryID, countrylanguage.languageID);
            return Ok(UpdatedItem);
        }

        // DELETE: api/Countries/5
        [ResponseType(typeof(CountryLanguageItem))]
        [Route("api/countryLanguages/{countryID}/{languageID}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCountry(System.String countryID, System.String languageID)
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

            CountryLanguage countryLanguage = db.CountryLanguages.Where(a => a.countryID == countryID && a.languageID == languageID).Include(b => b.country).Include(b => b.language).SingleOrDefault();
            if (countryLanguage == null)
            {
                return NotFound();
            }

            // Housekeeping
            countryLanguage.recStatus = -1;
            countryLanguage.recModifyBy = userName;
            countryLanguage.recModifyWhen = DateTime.Now;
            db.Entry(countryLanguage).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            // Reload the full version of the record
            db.Entry(countryLanguage).Reload();
            CountryLanguageItem UpdatedItem = GetCountryLanguageItem(countryID, languageID);
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

        private bool CountryLanguageExists(System.String countryID, System.String languageID)
        {
            return db.CountryLanguages.Count(e => e.countryID == countryID && e.languageID == languageID) > 0;
        }
        private CountryLanguageItem GetCountryLanguageItem(System.String countryID, System.String languageID)
        {
            var countryLanguage = db.CountryLanguages.Where(a => a.countryID == countryID && a.languageID == languageID).Include(b => b.country).Include(b => b.language).SingleOrDefault();
            CountryLanguageItem MyItem = new CountryLanguageItem();

            MyItem.countryID = countryLanguage.countryID;
            MyItem.fkCountryName = countryLanguage.country.countryName;
            MyItem.languageID = countryLanguage.languageID;
            MyItem.fkLanguageName = countryLanguage.language.languageName;
            MyItem.countryName = countryLanguage.countryName;
            MyItem.recStatus = countryLanguage.recStatus;
            MyItem.recCreatedBy = countryLanguage.recCreatedBy;
            MyItem.recCreatedWhen = countryLanguage.recCreatedWhen;
            MyItem.recModifyBy = countryLanguage.recModifyBy;
            MyItem.recModifyWhen = countryLanguage.recModifyWhen;

            return (MyItem);
        }

        private ValidationResult ValidateCountryLanguage(CountryLanguage countryLanguage, ApiAction action)
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

            if (action == ApiAction.Insert)
            {
                if (CountryLanguageExists(countryLanguage.countryID, countryLanguage.languageID))
                {
                    thisResult.Add(1, "Country Translation for " + countryLanguage.countryID.ToString() + " in " + countryLanguage.languageID + " already exists.");
                    return thisResult;
                }
            }
            else
            {
                if (!CountryLanguageExists(countryLanguage.countryID, countryLanguage.languageID))
                {
                    thisResult.Add(2, "Country Translation for country " + countryLanguage.countryID.ToString() + " in language " + countryLanguage.languageID + " does not exists.");
                    return thisResult;
                }
            }

            Country FKcountry = db.Countries.Find(countryLanguage.countryID);
            if (FKcountry == null)
            {
                thisResult.Add(4, "Country with ID " + countryLanguage.countryID.ToString() + " does not exist.");
                return thisResult;
            }
            Language FKlanguage = db.Languages.Find(countryLanguage.languageID);
            if (FKlanguage == null)
            {
                thisResult.Add(5, "Language with ID " + countryLanguage.languageID.ToString() + " does not exist.");
                return thisResult;
            }
            return thisResult;
        }
    }
}