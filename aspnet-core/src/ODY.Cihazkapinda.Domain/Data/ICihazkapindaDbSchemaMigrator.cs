using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Data
{
    public interface ICihazkapindaDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
