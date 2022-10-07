using AVIS.CoreBase;
using AVIS.CoreBase.Interfaces;
using System.Data;
using System.Data.SqlClient;

using Avis.Catalogo.Domain;
using Avis.Catalogo.Application;

namespace Avis.Catalogo.Infrastructure
{
    public class DapperUnitofWork : UnitofWorkBase, IDapperUnitofWork
    {
        public IQryRepository<Auto> AutoQryRepository { get; private set; }
        public ICmdRepository<Auto> AutoCmdRepository { get; private set; }
        
        public IInMemoryRepository<Auto> InMemoryRepository { get; private set; }


        public DapperUnitofWork(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(dbTransaction)
        {
            AutoQryRepository = RepositoryResolver.GetQryRepositoryInstance<Auto>(sqlConnection, dbTransaction);
            AutoCmdRepository = RepositoryResolver.GetCmdRepositoryInstance<Auto>(sqlConnection, dbTransaction);

            InMemoryRepository = RepositoryResolver.GetInMemoryRepositoryInstance<Auto>();
        }
    }
}
