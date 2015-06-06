namespace Concept.Repository.EF
{
	using Infrastructure.Common.Domain;
	using Infrastructure.Common.UnitOfWork;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using System.Transactions;

	public class UnitOfWork : IUnitOfWork
	{
		#region Declarations

		private readonly Dictionary<IAggregateRoot, IUnitOfWorkRepository> insertedAggregates;
		private readonly Dictionary<IAggregateRoot, IUnitOfWorkRepository> updatedAggregates;
		private readonly Dictionary<IAggregateRoot, IUnitOfWorkRepository> deletedAggregates;

		#endregion

		#region Constructors

		public UnitOfWork()
		{
			this.insertedAggregates = new Dictionary<IAggregateRoot, IUnitOfWorkRepository>();
			this.updatedAggregates = new Dictionary<IAggregateRoot, IUnitOfWorkRepository>();
			this.deletedAggregates = new Dictionary<IAggregateRoot, IUnitOfWorkRepository>();
		}

		#endregion

		#region Implementations

		public void RegisterUpdate(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository)
		{
			if (!this.updatedAggregates.ContainsKey(aggregateRoot))
			{
				this.updatedAggregates.Add(aggregateRoot, repository);
			}
		}

		public void RegisterInsert(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository)
		{
			if (!this.insertedAggregates.ContainsKey(aggregateRoot))
			{
				this.insertedAggregates.Add(aggregateRoot, repository);
			}
		}

		public void RegisterDelete(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository)
		{
			if (!this.deletedAggregates.ContainsKey(aggregateRoot))
			{
				this.deletedAggregates.Add(aggregateRoot, repository);
			}
		}

		public async Task CommitAsync()
		{
			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				foreach (IAggregateRoot aggregateRoot in this.insertedAggregates.Keys)
				{
					await this.insertedAggregates[aggregateRoot].PersistInsertAsync(aggregateRoot);
				}

				foreach (IAggregateRoot aggregateRoot in this.updatedAggregates.Keys)
				{
					await this.updatedAggregates[aggregateRoot].PersistUpdateAsync(aggregateRoot);
				}

				foreach (IAggregateRoot aggregateRoot in this.deletedAggregates.Keys)
				{
					await this.deletedAggregates[aggregateRoot].PersistDeleteAsync(aggregateRoot);
				}

				scope.Complete();
			}
		}

		#endregion
	}
}
