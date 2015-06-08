namespace Concept.Domain.Country
{
	using Infrastructure.Common.Domain;
	using Language;

	public class CountryLanguage : EntityBase<CountryLanguageID>, IAggregateRoot
	{
		#region Properties

		/// <summary>
		/// Gets or sets country name.
		/// </summary>
		public string CountryName { get; set; }

		/// <summary>
		/// Gets or sets country.
		/// </summary>
		public Country Country { get; set; }

		/// <summary>
		/// Gets or sets language.
		/// </summary>
		public Language Language { get; set; }

		#endregion

		#region Implementations

		public override void Validate()
		{
			if (this.ID == null)
			{
				this.AddBrokenRule(CountryLanguageBusinessRule.CountryLanguageIDRequired);
			}
			else
			{
				if (string.IsNullOrWhiteSpace(this.ID.CountryID))
				{
					this.AddBrokenRule(CountryLanguageBusinessRule.CountryIDInCountryLanguageIDRequired);
				}

				if (string.IsNullOrWhiteSpace(this.ID.LanguageID))
				{
					this.AddBrokenRule(CountryLanguageBusinessRule.LanguageIDInCountryLanguageIDRequired);
				}
			}

			if (string.IsNullOrWhiteSpace(this.CountryName))
			{
				this.AddBrokenRule(CountryLanguageBusinessRule.CountryNameInCountryLanguageRequired);
			}
		}

		#endregion
	}
}
