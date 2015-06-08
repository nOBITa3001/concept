namespace Concept.Domain.Country
{
	using Continent;
	using Currency;
	using Infrastructure.Common.Domain;
	using Language;
	using System;

	public class Country : EntityBase<string>, IAggregateRoot
	{
		#region Properties

		/// <summary>
		/// Gets or sets country name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets continent ID.
		/// </summary>
		public string ContinentID { get; set; }

		/// <summary>
		/// Gets or sets continent.
		/// </summary>
		public Continent Continent { get; set; }

		/// <summary>
		/// Gets or sets language ID.
		/// </summary>
		public string LanguageID { get; set; }

		/// <summary>
		/// Gets or sets language.
		/// </summary>
		public Language Language { get; set; }

		/// <summary>
		/// Gets or sets currency ID.
		/// </summary>
		public string CurrencyID { get; set; }

		/// <summary>
		/// Gets or sets currency.
		/// </summary>
		public Currency Currency { get; set; }

		#endregion

		#region Implementations

		public override void Validate()
		{
			if (string.IsNullOrWhiteSpace(this.ID))
			{
				this.AddBrokenRule(CountryBusinessRule.CountryIDRequired);
			}

			if (string.IsNullOrWhiteSpace(this.Name))
			{
				this.AddBrokenRule(CountryBusinessRule.CountryNameRequired);
			}

			if (string.IsNullOrWhiteSpace(this.ContinentID))
			{
				this.AddBrokenRule(CountryBusinessRule.ContinentIDInCountryRequired);
			}

			if (string.IsNullOrWhiteSpace(this.LanguageID))
			{
				this.AddBrokenRule(CountryBusinessRule.LanguageIDInCountryRequired);
			}

			if (string.IsNullOrWhiteSpace(this.CurrencyID))
			{
				this.AddBrokenRule(CountryBusinessRule.CurrencyIDInCountryRequired);
			}

			if (string.IsNullOrWhiteSpace(this.RecCreatedBy))
			{
				this.AddBrokenRule(CountryBusinessRule.RecordCreatedByRequired);
			}

			if (this.RecCreatedWhen == DateTime.MinValue)
			{
				this.AddBrokenRule(CountryBusinessRule.RecordCreatedWhenRequired);
			}
		}

		#endregion
	}
}
