using AVIS.CoreBase.Interfaces;

//Dependencia de arquitectura
using Avis.Catalogo.Domain;

namespace Avis.Catalogo.Application;

public interface IAutoService : IGenericService
{
    Task<IList<AutoDTO>> GetAllAsync(string filtro = "");

    Task<int> CreateAsync(AutoDTO auto);

    Task<AutoDTO> GetbyIdAsync(int id);
}