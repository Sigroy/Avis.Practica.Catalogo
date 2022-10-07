using AVIS.CoreBase.Interfaces;

using Avis.Catalogo.Domain;

namespace Avis.Catalogo.Application
{
    public interface IDapperUnitofWork: IUnitofWork
    {
        IQryRepository<Auto> AutoQryRepository { get; }
        ICmdRepository<Auto> AutoCmdRepository { get; }

        IInMemoryRepository<Auto> InMemoryRepository { get; }
    }
}
