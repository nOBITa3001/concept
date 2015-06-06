namespace Concept.Domain.Continent
{
	using Infrastructure.Common.Domain;
	using System;

	public class Continent : EntityBase<string>, IAggregateRoot
	{
		#region Properties

		/// <summary>
		/// Gets or sets continent name.
		/// </summary>
		public string Name { get; set; }

		#endregion

		#region Implementations

		public override void Validate()
		{
			if (string.IsNullOrWhiteSpace(this.ID))
			{
				this.AddBrokenRule(ContinentBusinessRule.ContinentIDRequired);
			}

			if (string.IsNullOrWhiteSpace(this.Name))
			{
				this.AddBrokenRule(ContinentBusinessRule.ContinentNameRequired);
			}

			if (string.IsNullOrWhiteSpace(this.RecCreatedBy))
			{
				this.AddBrokenRule(ContinentBusinessRule.RecordCreatedByRequired);
			}

			if (this.RecCreatedWhen == DateTime.MinValue)
			{
				this.AddBrokenRule(ContinentBusinessRule.RecordCreatedWhenRequired);
			}
		}

		#endregion
	}
}
