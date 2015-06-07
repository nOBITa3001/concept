namespace Concept.Domain.Language
{
	using Infrastructure.Common.Domain;
	using System;

	public class Language : EntityBase<string>, IAggregateRoot
	{
		#region Properties

		/// <summary>
		/// Gets or sets language name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets language native name.
		/// </summary>
		public string NativeName { get; set; }

		/// <summary>
		/// Gets or sets language fall back.
		/// </summary>
		public string Fallback { get; set; }

		#endregion

		#region Implementations

		public override void Validate()
		{
			if (string.IsNullOrWhiteSpace(this.ID))
			{
				this.AddBrokenRule(LanguageBusinessRule.LanguageIDRequired);
			}

			if (string.IsNullOrWhiteSpace(this.Name))
			{
				this.AddBrokenRule(LanguageBusinessRule.LanguageNameRequired);
			}

			if (string.IsNullOrWhiteSpace(this.NativeName))
			{
				this.AddBrokenRule(LanguageBusinessRule.LanguageNativeNameRequired);
			}

			if (string.IsNullOrWhiteSpace(this.Fallback))
			{
				this.AddBrokenRule(LanguageBusinessRule.LanguageFallbackRequired);
			}

			if (string.IsNullOrWhiteSpace(this.RecCreatedBy))
			{
				this.AddBrokenRule(LanguageBusinessRule.RecordCreatedByRequired);
			}

			if (this.RecCreatedWhen == DateTime.MinValue)
			{
				this.AddBrokenRule(LanguageBusinessRule.RecordCreatedWhenRequired);
			}
		}

		#endregion
	}
}
