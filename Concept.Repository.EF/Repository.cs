namespace Concept.Repository.EF
{
	using Infrastructure.Common.Domain;
	using Infrastructure.Common.UnitOfWork;
	using Models;
	using System;
	using System.Threading.Tasks;

	public abstract class Repository<DomainType, IDType> : IUnitOfWorkRepository
		where DomainType : IAggregateRoot
	{
		#region Declarations

		private readonly IUnitOfWork unitOfWork;
		private readonly ConceptContext dbContext;

		#endregion

		#region Properties

		/// <summary>
		/// Gets unit of work.
		/// </summary>
		public IUnitOfWork UnitOfWork
		{
			get
			{
				return this.unitOfWork;
			}
		}

		/// <summary>
		/// Gets database context.
		/// </summary>
		public ConceptContext DbContext
		{
			get
			{
				return this.dbContext;
			}
		}

		#endregion

		#region Constructors

		public Repository(IUnitOfWork unitOfWork, IObjectContextFactory objectContextFactory)
		{
			this.unitOfWork = unitOfWork;
			this.dbContext = objectContextFactory.Create();

			this.ThrowExceptionIfRepositoryIsInvalid();
		}

		#endregion

		#region Implementations

		public abstract DomainType Get(IDType id);

		public abstract Task PersistInsertAsync(IAggregateRoot aggregateRoot);
		public abstract Task PersistUpdateAsync(IAggregateRoot aggregateRoot);
		public abstract Task PersistDeleteAsync(IAggregateRoot aggregateRoot);

		#endregion

		#region Methods

		private void ThrowExceptionIfRepositoryIsInvalid()
		{
			if (this.unitOfWork == null)
			{
				throw new ArgumentNullException("UnitOfWork in Repository");
			}

			if (this.dbContext == null)
			{
				throw new ArgumentNullException("DBContext in Repository");
			}
		}

		public void Insert(DomainType aggregate)
		{
			this.unitOfWork.RegisterInsert(aggregate, this);
		}

		public void Update(DomainType aggregate)
		{
			this.unitOfWork.RegisterUpdate(aggregate, this);
		}

		public void Delete(DomainType aggregate)
		{
			this.unitOfWork.RegisterDelete(aggregate, this);
		}

		#endregion
	}
}
