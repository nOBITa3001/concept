namespace Concept.Infrastructure.Common.Domain
{
	/// <summary>
	/// Rule of business class.
	/// </summary>
	public class BusinessRule
	{
		#region Declarations

		private string ruleDescription;

		#endregion

		#region Properties

		/// <summary>
		/// Gets a rule description.
		/// </summary>
		public string RuleDescription
		{
			get
			{
				return this.ruleDescription;
			}
		}

		#endregion

		#region Constructors
		
		/// <summary>
		/// Initialize a BusinessRule class.
		/// </summary>
		public BusinessRule(string ruleDescription)
		{
			this.ruleDescription = ruleDescription;
		}

		#endregion
	}
}
