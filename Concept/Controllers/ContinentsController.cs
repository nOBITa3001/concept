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

namespace Concept.Controllers
{
    public class ContinentsController : ApiController
    {
        private ConceptContext db = new ConceptContext();

        // GET: api/Continents
        [Route("api/continents")]
        [HttpGet]
        public IQueryable<ContinentList> GetContinents(System.Boolean displayAll = false)
        {
            var continent = from b in db.Continents
                            select new ContinentList()
                            {
                                continentID = b.continentID,
                                continentName = b.continentName,
                                recStatus = b.recStatus
                            };
            if (displayAll == false)
                continent = continent.Where(b => b.recStatus > 0);
            continent = continent.OrderBy(b => b.continentName);
            return continent;
        }

        // GET: api/Continents/5
        [ResponseType(typeof(Continent))]
        [Route("api/continents/{continentID}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetContinent(string continentID)
        {
            var continent = await db.Continents.Select(b =>
               new ContinentItem()
               {
                   continentID = b.continentID,
                   continentName = b.continentName,
                   recStatus = b.recStatus,
                   recCreatedBy = b.recCreatedBy,
                   recCreatedWhen = b.recCreatedWhen,
                   recModifyBy = b.recModifyBy,
                   recModifyWhen = b.recModifyWhen
               }).SingleOrDefaultAsync(b => b.continentID == continentID);
            if (continent == null)
            {
                return NotFound();
            }
            return Ok(continent);
        }

        // POST: api/Continents
        [ResponseType(typeof(Continent))]
        [Route("api/continents")]
        [HttpPost]
        public async Task<IHttpActionResult> PostContinent(Continent continent)
        {
            ValidationResult MyValidation = ValidateContinent(continent, ApiAction.Insert);
            if (!MyValidation.isSuccess())
            {
                return BadRequest(MyValidation.ToJson());
            }
            String userName = Request.Headers.GetValues("username").FirstOrDefault();

            //// HouseKeeping
            continent.recCreatedBy = userName;
            continent.recCreatedWhen = DateTime.Now;
            db.Continents.Add(continent);
            await db.SaveChangesAsync();
            return Ok(GetContinentItem(continent.continentID));
        }

        // PUT: api/Continents/5
        [ResponseType(typeof(void))]
        [Route("api/continents/{continentID}")]
        [HttpPut]
        public async Task<IHttpActionResult> PutContinent(string continentID, Continent continent)
        {
            if (continentID != continent.continentID)
            {
                return BadRequest("URL continent and Data continent conflict!");
            }
            ValidationResult MyValidation = ValidateContinent(continent, ApiAction.Update);
            if (!MyValidation.isSuccess())
            {
                return BadRequest(MyValidation.ToJson());
            }
            String userName = Request.Headers.GetValues("username").FirstOrDefault();

            continent.recModifyBy = userName;
            continent.recModifyWhen = DateTime.Now;
            db.Entry(continent).State = EntityState.Modified;
            db.Entry(continent).Property(x => x.recCreatedWhen).IsModified = false;
            db.Entry(continent).Property(x => x.recCreatedBy).IsModified = false;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContinentExists(continentID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // Reload the full version of the record
            db.Entry(continent).Reload();
            ContinentItem UpdatedItem = GetContinentItem(continentID);
            return Ok(UpdatedItem);
        }

        // DELETE: api/Continents/5
        [ResponseType(typeof(Continent))]
        [Route("api/continents/{continentID}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteContinent(string continentID)
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

            Continent continent = await db.Continents.FindAsync(continentID);
            if (continent == null)
            {
                return NotFound();
            }

            // Housekeeping
            continent.recStatus = -1;
            continent.recModifyBy = userName;
            continent.recModifyWhen = DateTime.Now;
            db.Entry(continent).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            // Reload the full version of the record
            ContinentItem UpdatedItem = GetContinentItem(continentID);
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

        private bool ContinentExists(string id)
        {
            return db.Continents.Count(e => e.continentID == id) > 0;
        }

        private ValidationResult ValidateContinent(Continent continent, ApiAction action)
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

            bool Exists = ContinentExists(continent.continentID);
            if (Exists && action == ApiAction.Insert)
            {
                thisResult.Add(2, "Continent with ID " + continent.continentID.ToString() + " alreday exists.");
                return thisResult;
            }
            if (!Exists && action == ApiAction.Update)
            {
                thisResult.Add(2, "Continent with ID " + continent.continentID.ToString() + " does not exists.");
                return thisResult;
            }
            return thisResult;
        }
        private ContinentItem GetContinentItem(System.String continentID)
        {
            var continent = db.Continents.Where(a => a.continentID == continentID).SingleOrDefault();
            ContinentItem MyItem = new ContinentItem();

            MyItem.continentID = continent.continentID;
            MyItem.continentName = continent.continentName;
            MyItem.recStatus = continent.recStatus;
            MyItem.recCreatedBy = continent.recCreatedBy;
            MyItem.recCreatedWhen = continent.recCreatedWhen;
            MyItem.recModifyBy = continent.recModifyBy;
            MyItem.recModifyWhen = continent.recModifyWhen;

            return (MyItem);
        }
    }
}