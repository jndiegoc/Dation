using DationBusFunction.Model.DBModel;

namespace DationBusFunction.DAL.Definitions
{
    public interface IApplicationDbContextData
    {
        ApplicationDbContext applicationDbContext(string clientTenant);
    }
}
