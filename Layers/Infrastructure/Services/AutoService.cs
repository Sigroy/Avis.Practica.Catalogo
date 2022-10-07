using AVIS.CoreBase.Clases;
using AVIS.CoreBase.Extensions;
using AVIS.CoreBase.Interfaces;

using System.Data;
using System.Data.SqlClient;

using Avis.Catalogo.Application;
using Avis.Catalogo.Domain;

namespace Avis.Catalogo.Infrastructure
{
    public class AutoService : IAutoService
    {

        private readonly IAutoAggregate _auto;

        public readonly IQryRepository<Auto> _qryRepository;
        public readonly ICmdRepository<Auto> _cmdRepository;

        public IList<InternalException> Errores { get; } = new List<InternalException>();

        public bool Success { get; private set; } = false;

        // private bool disposedValue;

        public AutoService(
            IAutoAggregate auto,
            SqlConnection sqlConnection,
            IDbTransaction dbTransaction
            )
        {
            _auto = auto;

            _qryRepository = RepositoryResolver.GetQryRepositoryInstance<Auto>(sqlConnection, dbTransaction);
            _cmdRepository = RepositoryResolver.GetCmdRepositoryInstance<Auto>(sqlConnection, dbTransaction);
        }

        public async Task<IList<AutoDTO>> GetAllAsync(string filtro = "")
        {
            Success = true;
            IList<AutoDTO> lista = new List<AutoDTO>();
            try
            {
                //dapper
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
                Success=false;
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
            Success=true;
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
            catch (Exception e)
            {
                Success = false;
                string extra = "";
                if (e.InnerException != null)
                {
                    extra = e.InnerException.Message;
                }
                InternalException error = new InternalException()
                {
                    ClassName = this.GetType().ToString(),
                    MethodName = "Add",
                    ErrorMessage = "Inner:" + extra + " Exception:" + e.Message,
                    Ex = e
                };
                Errores.Add(error);
            }
            return id;
        }

        #region GETBYFILTERASYNC
        //public async Task<AutoDTO> GetByFilterAsync(int id)
        //{
        //    Success = true;
        //    AutoDTO item = null;
        //    try
        //    {
        //        string filtro = $" AutoID = {id}";
        //        var elemento = await _qryRepository.GetByFilterAsync(filtro);
        //        if (_qryRepository.Success)
        //        {
        //            item = elemento.ToModelorVM<AutoDTO>();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Success = false;
        //        string extra = "";
        //        if (ex.InnerException != null)
        //        {
        //            extra = ex.InnerException.Message;
        //        }
        //        InternalException error = new InternalException()
        //        {
        //            ClassName = this.GetType().ToString(),
        //            MethodName = "Get",
        //            ErrorMessage = "Inner:" + extra + " Exception:" + ex.Message,
        //            Ex = ex
        //        };
        //        Errores.Add(error);
        //    }
        //    return item;
        //}
        #endregion

        #region UPDATEASYNC
        //public async Task<int> UpdateAsync(AutoDTO auto)
        //{
        //    Success = true;
        //    int id = 0;
        //    try
        //    {
        //        var coche = await _qryRepository.GetByFilterAsync($"AutoPKey = {auto.AutoPKey}");
        //        Auto elemento = auto.ToModelorVM<Auto>();
        //        coche.UpdateInfo(elemento);
        //        id = await _cmdRepository.UpdateAsync(coche);
        //    }
        //    catch (Exception ex)
        //    {
        //        Success = false;
        //        string extra = "";
        //        if (ex.InnerException != null)
        //        {
        //            extra = ex.InnerException.Message;
        //        }
        //        InternalException error = new InternalException()
        //        {
        //            ClassName = this.GetType().ToString(),
        //            MethodName = "Get",
        //            ErrorMessage = "Inner:" + extra + " Exception:" + ex.Message,
        //            Ex = ex
        //        };
        //        Errores.Add(error);
        //    }
        //    return id;
        //}
        #endregion

        #region DISPOSE
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        if (disposing)
        //        {

        //        }
        //        disposedValue = true;
        //    }
        //}

        //void IDisposable.Dispose()
        //{
        //    Dispose(disposing: true);
        //    GC.SuppressFinalize(this);
        //}
        #endregion

    }
}
