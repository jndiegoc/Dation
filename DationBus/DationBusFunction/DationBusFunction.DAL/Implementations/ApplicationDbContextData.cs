using DationBusFunction.DAL.Definitions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DationBusFunction.Model.DBModel;

namespace DationBusFunction.DAL.Implementations
{
    public class ApplicationDbContextData : IApplicationDbContextData
    {
        private ApplicationDbContext _applicationDbContext;

        public ApplicationDbContext applicationDbContext(string dbConnectionString)
        {

            if (null != _applicationDbContext)
                _applicationDbContext = null;

            SqlConnectionStringBuilder sqlConnStrBuilder = new SqlConnectionStringBuilder(dbConnectionString);
            sqlConnStrBuilder.ConnectTimeout = 500;
            DbContextOptionsBuilder<ApplicationDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            dbContextOptionsBuilder.UseSqlServer(sqlConnStrBuilder.ConnectionString);
            _applicationDbContext = new ApplicationDbContext(dbContextOptionsBuilder.Options);


            return _applicationDbContext;
        }
    }
}
