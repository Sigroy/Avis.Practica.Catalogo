using AVIS.CoreBase.Interfaces;

// Dependencia de arquitectura
using Avis.Catalogo.Domain;

namespace Avis.Catalogo.Application;

// Extensión de la unidad de trabajo

public interface IDapperUnitofWork: IUnitofWork
{
    IQryRepository<Auto> AutoQryRepository { get; }
    ICmdRepository<Auto> AutoCmdRepository { get;  }

    IInMemoryRepository<Auto> InMemoryRepository { get; }
}

