namespace Concept.Domain.Country
{
	using Infrastructure.Common.Domain;

	internal static class CountryLanguageBusinessRule
	{
		/// <summary>
		/// Business rule of country language ID required.
		/// </summary>
		public static readonly BusinessRule CountryLanguageIDRequired = new BusinessRule("A country language must has an ID.");

		/// <summary>
		/// Business rule of country ID in country language ID required.
		/// </summary>
		public static readonly BusinessRule CountryIDInCountryLanguageIDRequired = new BusinessRule("A country language ID must has a country ID.");

		/// <summary>
		/// Business rule of language ID in country language ID required.
		/// </summary>
		public static readonly BusinessRule LanguageIDInCountryLanguageIDRequired = new BusinessRule("A country language ID must has a language ID.");

		/// <summary>
		/// Business rule of country name in country language required.
		/// </summary>
		public static readonly BusinessRule CountryNameInCountryLanguageRequired = new BusinessRule("A country language must has a country name.");
	}
}
