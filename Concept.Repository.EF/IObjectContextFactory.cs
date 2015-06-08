namespace Concept.Repository.EF
{
	using Models;

	public interface IObjectContextFactory
	{
		ConceptContext Create();
	}
}