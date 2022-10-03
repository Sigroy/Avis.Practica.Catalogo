using AVIS.CoreBase.Interfaces;

// Dependencia de arquitectura
using Avis.Catalogo.Domain;

namespace Avis.Catalogo.Application;

// Extensión de la unidad de trabajo

public interface IDapperUnitofWork: IDapperUnitofWork
{
    IQryRepository<Auto> ResQryRepository { get; }
    ICmdRepository<Auto> ResCmdRepository { get;  }

    IInMemoryRepository<Auto> InMemoryRepository { get; }
}

