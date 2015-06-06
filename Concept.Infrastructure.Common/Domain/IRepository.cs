namespace Concept.Infrastructure.Common.Domain
{
	public interface IRepository<AggregateType, IDType> : IReadOnlyRepository<AggregateType, IDType>
		where AggregateType : IAggregateRoot
	{
		void Update(AggregateType aggregate);
		void Insert(AggregateType aggregate);
		void Delete(AggregateType aggregate);
	}
}
