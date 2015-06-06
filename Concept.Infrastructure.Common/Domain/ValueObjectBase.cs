namespace Concept.Infrastructure.Common.Domain
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Value object base class.
	/// </summary>
	public abstract class ValueObjectBase
	{
		#region Declarations

		private IList<BusinessRule> brokenRules = new List<BusinessRule>();

		#endregion

		#region Implementations

		/// <summary>
		/// Validate the value object.
		/// </summary>
		protected abstract void Validate();

		#endregion

		#region Methods

		/// <summary>
		/// Sets a broken business rule into broken rule list.
		/// </summary>
		protected void AddBrokenRule(BusinessRule businessRule)
		{
			this.brokenRules.Add(businessRule);
		}

		/// <summary>
		/// Throw an expection if value object is invalid.
		/// </summary>
		public void ThrowExceptionIfInvalid()
		{
			// We first clear the list so that we don’t return any previously stored broken rules.
			// They may have been fixed by then. 
			this.brokenRules.Clear();

			// We then run the Validate method which is implemented in the concrete domain classes.
			// The domain will fill up the list of broken rules in that implementation.
			this.Validate();

			if (this.brokenRules != null && this.brokenRules.Any())
			{
				throw new ValueObjectIsInvalidException(this.GetBrokenRulesString());
			}
		}

		/// <summary>
		/// Gets the broken business rule string.
		/// </summary>
		private string GetBrokenRulesString()
		{
			StringBuilder result = new StringBuilder();
			foreach (BusinessRule businessRule in this.brokenRules)
			{
				result.AppendLine(businessRule.RuleDescription);
			}

			return result.ToString();
		}

		#endregion
	}
}
