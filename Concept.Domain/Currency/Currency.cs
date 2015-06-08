namespace Concept.Domain.Currency
{
	using Infrastructure.Common.Domain;
	using System;

	public class Currency : EntityBase<string>, IAggregateRoot
	{
		#region Properties

		/// <summary>
		/// Gets or sets currency name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets currency sign.
		/// </summary>
		public string Sign { get; set; }

		/// <summary>
		/// Gets or sets currency sign position.
		/// </summary>
		public string SignPosition { get; set; }

		/// <summary>
		/// Gets or sets currency up lift.
		/// </summary>
		public decimal Uplift { get; set; }

		#endregion

		#region Implementations

		public override void Validate()
		{
			if (string.IsNullOrWhiteSpace(this.ID))
			{
				this.AddBrokenRule(CurrencyBusinessRule.CurrencyIDRequired);
			}

			if (string.IsNullOrWhiteSpace(this.Name))
			{
				this.AddBrokenRule(CurrencyBusinessRule.CurrencyNameRequired);
			}

			if (string.IsNullOrWhiteSpace(this.Sign))
			{
				this.AddBrokenRule(CurrencyBusinessRule.CurrencySignRequired);
			}

			if (string.IsNullOrWhiteSpace(this.SignPosition))
			{
				this.AddBrokenRule(CurrencyBusinessRule.CurrencySignPositionRequired);
			}

			if (string.IsNullOrWhiteSpace(this.RecCreatedBy))
			{
				this.AddBrokenRule(CurrencyBusinessRule.RecordCreatedByRequired);
			}

			if (this.RecCreatedWhen == DateTime.MinValue)
			{
				this.AddBrokenRule(CurrencyBusinessRule.RecordCreatedWhenRequired);
			}
		}

		#endregion
	}
}
