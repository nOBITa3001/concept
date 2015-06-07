namespace Concept.Domain.Currency
{
	using Infrastructure.Common.Domain;

	internal class CurrencyBusinessRule
	{
		/// <summary>
		/// Business rule of currency ID required.
		/// </summary>
		public static readonly BusinessRule CurrencyIDRequired = new BusinessRule("A currency must has an ID");

		/// <summary>
		/// Business rule of currency name required.
		/// </summary>
		public static readonly BusinessRule CurrencyNameRequired = new BusinessRule("A currency must has a name");

		/// <summary>
		/// Business rule of currency sign required.
		/// </summary>
		public static readonly BusinessRule CurrencySignRequired = new BusinessRule("A currency must has a sign");

		/// <summary>
		/// Business rule of currency sign position required.
		/// </summary>
		public static readonly BusinessRule CurrencySignPositionRequired = new BusinessRule("A currency must has a sign position");

		/// <summary>
		/// Business rule of currency record created by required.
		/// </summary>
		public static readonly BusinessRule RecordCreatedByRequired = new BusinessRule("A currency must has a record created by");

		/// <summary>
		/// Business rule of currency record created when required.
		/// </summary>
		public static readonly BusinessRule RecordCreatedWhenRequired = new BusinessRule("A currency must has a record created when");
	}
}