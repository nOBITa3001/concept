namespace Concept.Infrastructure.Common.Domain
{
	using System.Collections.Generic;

	public interface IReadOnlyRepository<AggregateType, IDType>
		where AggregateType : IAggregateRoot
	{
		AggregateType Get(IDType id);
		IEnumerable<AggregateType> GetAll();
	}
}
