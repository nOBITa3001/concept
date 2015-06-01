using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Concept.Models
{
    public class ConceptContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ConceptContext() : base("name=ConceptContext")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().ToTable("Language", schemaName: "core");
            modelBuilder.Entity<Currency>().ToTable("Currency", schemaName: "core");
            modelBuilder.Entity<Continent>().ToTable("Continent", schemaName: "geograph");
            modelBuilder.Entity<Country>().ToTable("Country", schemaName: "geograph");
            modelBuilder.Entity<CountryLanguage>().ToTable("Country_language", schemaName: "geograph");
        }
        public System.Data.Entity.DbSet<Concept.Models.Continent> Continents { get; set; }

        public System.Data.Entity.DbSet<Concept.Models.Currency> Currencies { get; set; }

        public System.Data.Entity.DbSet<Concept.Models.Language> Languages { get; set; }

        public System.Data.Entity.DbSet<Concept.Models.Country> Countries { get; set; }
        public System.Data.Entity.DbSet<Concept.Models.CountryLanguage> CountryLanguages { get; set; }
    }
}
