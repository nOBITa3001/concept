namespace Concept.Application.Service.Implementation
{
	using Infrastructure.Common.UnitOfWork;
	using System;

	public abstract class ApplicationServiceBase
	{
		#region Declarations

		private readonly IUnitOfWork unitOfWork;

		#endregion

		#region Properties

		public IUnitOfWork UnitOfWork
		{
			get
			{
				return this.unitOfWork;
			}
		}

		#endregion

		#region Constructors

		public ApplicationServiceBase(IUnitOfWork unitOfWork)
		{
			if (unitOfWork == null)
			{
				throw new ArgumentNullException("UnitOfWork");
			}

			this.unitOfWork = unitOfWork;
		}

		#endregion

		#region Implementations

		protected abstract void ThrowExceptionIfServiceIsInvalid();

		#endregion
	}
}
