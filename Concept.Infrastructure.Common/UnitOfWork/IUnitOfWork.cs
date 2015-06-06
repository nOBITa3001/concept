namespace Concept.Infrastructure.Common.UnitOfWork
{
	using Domain;
	using System.Threading.Tasks;

	public interface IUnitOfWork
	{
		void RegisterUpdate(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository);
		void RegisterInsert(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository);
		void RegisterDelete(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository);
		Task CommitAsync();
	}
}
