namespace Concept.Infrastructure.Common.Domain
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Entity base class.
	/// </summary>
	/// <typeparam name="IDType">Type of ID, such as int, Guid, and so on.</typeparam>
	public abstract class EntityBase<IDType>
	{
		#region Declarations

		private IList<BusinessRule> brokenRules = new List<BusinessRule>();

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets entity ID.
		/// </summary>
		public IDType ID { get; set; }

		/// <summary>
		/// Gets or sets entity record status.
		/// </summary>
		public int RecStatus { get; set; }

		/// <summary>
		/// Gets or sets entity record created by.
		/// </summary>
		public string RecCreatedBy { get; set; }

		/// <summary>
		/// Gets or sets entity record created when.
		/// </summary>
		public DateTime RecCreatedWhen { get; set; }

		/// <summary>
		/// Gets or sets entity record modify by.
		/// </summary>
		public string RecModifyBy { get; set; }

		/// <summary>
		/// Gets or sets entity record modify when.
		/// </summary>
		public DateTime? RecModifyWhen { get; set; }

		#endregion

		#region Implementations

		/// <summary>
		/// Determines whether this instance and another specified entity object have the same value.
		/// </summary>
		public override bool Equals(object entity)
		{
			return (entity != null
					&& entity is EntityBase<IDType>
					&& this == (EntityBase<IDType>)entity);
		}

		/// <summary>
		/// Serves as a hash function for an entity ID.
		/// </summary>
		public override int GetHashCode()
		{
			return this.ID.GetHashCode();
		}

		/// <summary>
		/// Determines whether this instance and another specified entity object have the same value.
		/// </summary>
		public static bool operator ==(EntityBase<IDType> entity1, EntityBase<IDType> entity2)
		{
			if ((object)entity1 == null && (object)entity2 == null)
			{
				return true;
			}

			if ((object)entity1 == null || (object)entity2 == null)
			{
				return false;
			}

			if (entity1.ID.ToString() == entity2.ID.ToString())
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Determines whether this instance and another specified entity object have a different value.
		/// </summary>
		public static bool operator !=(EntityBase<IDType> entity1, EntityBase<IDType> entity2)
		{
			return (!(entity1 == entity2));
		}

		/// <summary>
		/// Validate the entity.
		/// </summary>
		public abstract void Validate();

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
		/// Gets a broken business rule list.
		/// </summary>
		public IEnumerable<BusinessRule> GetBrokenRules()
		{
			// We first clear the list so that we don’t return any previously stored broken rules.
			// They may have been fixed by then. 
			this.brokenRules.Clear();

			// We then run the Validate method which is implemented in the concrete domain classes.
			// The domain will fill up the list of broken rules in that implementation.
			this.Validate();

			return this.brokenRules;
		}

		#endregion
	}
}
