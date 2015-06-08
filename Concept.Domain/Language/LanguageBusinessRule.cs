namespace Concept.Domain.Language
{
	using Infrastructure.Common.Domain;

	internal class LanguageBusinessRule
	{
		/// <summary>
		/// Business rule of language ID required.
		/// </summary>
		public static BusinessRule LanguageIDRequired = new BusinessRule("A language must has an ID");

		/// <summary>
		/// Business rule of language  required.
		/// </summary>
		public static BusinessRule LanguageNameRequired = new BusinessRule("A language must has a name");

		/// <summary>
		/// Business rule of language  required.
		/// </summary>
		public static BusinessRule LanguageNativeNameRequired = new BusinessRule("A language must has a native name");

		/// <summary>
		/// Business rule of language  required.
		/// </summary>
		public static BusinessRule LanguageFallbackRequired = new BusinessRule("A language must has a fallback");

		/// <summary>
		/// Business rule of language  required.
		/// </summary>
		public static BusinessRule RecordCreatedByRequired = new BusinessRule("A language must has a record created by");

		/// <summary>
		/// Business rule of language  required.
		/// </summary>
		public static BusinessRule RecordCreatedWhenRequired = new BusinessRule("A language must has a record created when");
	}
}