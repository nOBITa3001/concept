namespace Concept.Domain.Country
{
	using Infrastructure.Common.Domain;

	internal static class CountryBusinessRule
	{
		/// <summary>
		/// Business rule of country ID required.
		/// </summary>
		public static readonly BusinessRule CountryIDRequired = new BusinessRule("A country must has an ID.");

		/// <summary>
		/// Business rule of country name required.
		/// </summary>
		public static readonly BusinessRule CountryNameRequired = new BusinessRule("A country must has a name.");

		/// <summary>
		/// Business rule of continent ID in country required.
		/// </summary>
		public static readonly BusinessRule ContinentIDInCountryRequired = new BusinessRule("A country must has a continent ID.");

		/// <summary>
		/// Business rule of language ID in country required.
		/// </summary>
		public static readonly BusinessRule LanguageIDInCountryRequired = new BusinessRule("A country must has a language ID.");

		/// <summary>
		/// Business rule of currency ID in currency required.
		/// </summary>
		public static readonly BusinessRule CurrencyIDInCountryRequired = new BusinessRule("A country must has a currency ID.");

		/// <summary>
		/// Business rule of country record created by required.
		/// </summary>
		public static readonly BusinessRule RecordCreatedByRequired = new BusinessRule("A country must has a record created by");

		/// <summary>
		/// Business rule of country record created when required.
		/// </summary>
		public static readonly BusinessRule RecordCreatedWhenRequired = new BusinessRule("A country must has a record created when");
	}
}