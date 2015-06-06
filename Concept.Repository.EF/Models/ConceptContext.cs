namespace Concept.Repository.EF.Models
{
	using System.Data.Entity;

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
        public DbSet<Continent> Continents { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryLanguage> CountryLanguages { get; set; }
    }
}
