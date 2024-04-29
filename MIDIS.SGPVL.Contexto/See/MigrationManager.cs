using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Contexto.Seed.Maestros;

namespace MIDIS.SGPVL.Contexto.See
{
    public static class MigrationManager
    {
        public static async Task MigrateDatabaseAsync(this IHost host)
        {
            try
            {
                using (var scope = host.Services.CreateScope())
                {
                    using (var appContext = scope.ServiceProvider.GetRequiredService<BDPVLContext>())
                    {
                        appContext.Database.EnsureCreated();
                        {
                            appContext.Database.Migrate();
                        }
                        await appContext.Database.MigrateAsync();
                        await new InitMaestro(appContext).Create();
                        //await new InitSecurity(appContext).Create();
                    }
                }
                await host.RunAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
