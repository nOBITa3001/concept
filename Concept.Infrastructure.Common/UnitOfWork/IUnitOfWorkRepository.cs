namespace Concept.Infrastructure.Common.UnitOfWork
{
	using Domain;
	using System.Threading.Tasks;

	public interface IUnitOfWorkRepository
	{
		Task PersistInsertAsync(IAggregateRoot aggregateRoot);
		Task PersistUpdateAsync(IAggregateRoot aggregateRoot);
		Task PersistDeleteAsync(IAggregateRoot aggregateRoot);
	}
}
