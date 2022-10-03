using AVIS.CoreBase.Clases;
using AVIS.CoreBase.Extensions;
using AVIS.CoreBase.Interfaces;
using System.Data.SqlClient;
using System.Data;

//Dependencia Arquitectura
using Avis.Catalogo.Application;
using Avis.Catalogo.Domain;

namespace Avis.Catalogo.Infrastructure;

public class AutoService : IAutoService
{
    private readonly IAutoAggregate _auto;

    public readonly IQryRepository<Auto> _qryRepository;
    public readonly ICmdRepository<Auto> _cmdRepository;

    public IList<InternalException> Errores { get; } = new List<InternalException>();

    public bool Success { get; private set; } = false;

    public AutoService(
        IAutoAggregate auto,
        SqlConnection sqlConnection,
        IDbTransaction dbTransaction
    )
    {
        _auto = auto;
        //_unitofWork = unitofWork;
        _qryRepository = RepositoryResolver.GetQryRepositoryInstance<Auto>(sqlConnection, dbTransaction);
        _cmdRepository = RepositoryResolver.GetCmdRepositoryInstance<Auto>(sqlConnection, dbTransaction);
    }

    public async Task<IList<AutoDTO>> GetAllAsync(string filtro = "")
    {
        Success = true;
        IList<AutoDTO> lista = new List<AutoDTO>();
        try
        {
            //Dapper
            var temp = await _qryRepository.GetAllAsync();

            if (_qryRepository.Success)
            {
                lista = temp.ToListOfDestination<AutoDTO>();
            }
            else
            {
                Errores.ToList().AddRange(_qryRepository.Errores);
                Success = false;
            }
        }
        catch (Exception ex)
        {
            Success = false;
            string extra = "";
            if (ex.InnerException != null)
            {
                extra = ex.InnerException.Message;
            }

            InternalException error = new InternalException()
            {
                ClassName = this.GetType().ToString(),
                MethodName = "Index",
                ErrorMessage = "Inner:" + extra + " Exception:" + ex.Message,
                Ex = ex
            };
            Errores.Add(error);
        }

        return lista;
    }

    public async Task<AutoDTO> GetbyIdAsync(int id)
    {
        Success = true;
        AutoDTO item = null;
        try
        {
            var elemento = await _qryRepository.GetByIdAsync(id);
            if (_qryRepository.Success)
            {
                item = elemento.ToModelorVM<AutoDTO>();
            }
            else
            {
                Errores.ToList().AddRange(_qryRepository.Errores);
                Success = false;
            }
        }
        catch (Exception ex)
        {
            Success = false;
            string extra = "";
            if (ex.InnerException != null)
            {
                extra = ex.InnerException.Message;
            }

            InternalException error = new InternalException()
            {
                ClassName = this.GetType().ToString(),
                MethodName = "Get",
                ErrorMessage = "Inner:" + extra + " Exception:" + ex.Message,
                Ex = ex
            };
            Errores.Add(error);
        }

        return item;
    }

    public async Task<int> CreateAsync(AutoDTO auto)
    {
        Success = true;
        int id = 0;
        try
        {
            id = await _auto.CreateAsync(auto);
            if (!_auto.Success)
            {
                Errores.ToList().AddRange(_auto.Errores);
                Success = false;
            }
        }
        catch (Exception ex)
        {
            Success = false;
            string extra = "";
            if (ex.InnerException != null)
            {
                extra = ex.InnerException.Message;
            }

            InternalException error = new InternalException()
            {
                ClassName = this.GetType().ToString(),
                MethodName = "Add",
                ErrorMessage = "Inner:" + extra + " Exception:" + ex.Message,
                Ex = ex
            };
            Errores.Add(error);
        }

        return id;
    }
}