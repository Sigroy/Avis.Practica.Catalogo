using AVIS.CoreBase.Interfaces;
using Avis.Catalogo.Domain;


namespace Avis.Catalogo.Application
{
    public interface IAutoAggregate:IAggregate
    {
        Task<int> CreateAsync(AutoDTO auto);
    }
}
