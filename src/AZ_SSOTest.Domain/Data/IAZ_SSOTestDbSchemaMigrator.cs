using System.Threading.Tasks;

namespace AZ_SSOTest.Data;

public interface IAZ_SSOTestDbSchemaMigrator
{
    Task MigrateAsync();
}
