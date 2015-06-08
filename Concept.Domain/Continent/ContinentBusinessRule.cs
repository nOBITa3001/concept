namespace Concept.Domain.Continent
{
	using Infrastructure.Common.Domain;

	internal static class ContinentBusinessRule
	{
		/// <summary>
		/// Business rule of continent ID required.
		/// </summary>
		public static readonly BusinessRule ContinentIDRequired = new BusinessRule("A continent must has an ID.");

		/// <summary>
		/// Business rule of continent name required.
		/// </summary>
		public static readonly BusinessRule ContinentNameRequired = new BusinessRule("A continent must has a name.");

		/// <summary>
		/// Business rule of continent record created by required.
		/// </summary>
		public static readonly BusinessRule RecordCreatedByRequired = new BusinessRule("A continent must has a record created by");

		/// <summary>
		/// Business rule of continent record created when required.
		/// </summary>
		public static readonly BusinessRule RecordCreatedWhenRequired = new BusinessRule("A continent must has a record created when");
	}
}