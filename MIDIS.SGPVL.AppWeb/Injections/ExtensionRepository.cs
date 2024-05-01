
using Microsoft.EntityFrameworkCore;
using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Manager.ComiteAdmin;
using MIDIS.SGPVL.Manager.Maestro;
using MIDIS.SGPVL.Manager.Settings;
using MIDIS.SGPVL.Repository.Comite;
using MIDIS.SGPVL.Repository.ComitePvl;
using MIDIS.SGPVL.Repository.Maestro;
using MIDIS.SGPVL.Repository.Persona;
using MIDIS.SGPVL.Repository.UnitOfWork;

namespace MIDIS.SGPVL.AppWeb.Injections
{
    public static class ExtensionRepository
    {
        public class RepositoriesOptions
        {
            public string ConnectionString { get; set; }
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services, Action<RepositoriesOptions> configureOptions)
        {
            var options = new RepositoriesOptions();
            configureOptions(options);

            services.AddScoped<IComiteAdminMiembroRepository, ComiteAdminMiembroRepository>();
            services.AddScoped<IComiteAdminRepository, ComiteAdminRepository>();

            services.AddScoped<IComitePVLRepository, ComitePVLRepository>();
            services.AddScoped<IJunDirectivaRepository, JunDirectivaRepository>();
            services.AddScoped<IMiembroJuntaRepository, MiembroJuntaRepository>();
            services.AddScoped<ISocioReposiroty, SocioReposiroty>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<ICentroPobladoRepository,CentroPobladoRepository >();
            services.AddScoped<IDepartamentoRepository,DepartamentoRepository >();
            services.AddScoped<IEnumeradoItemRepository,EnumeradoItemRepository >();
            services.AddScoped<IEnumeradoRepository,EnumeradoRepository >();
            services.AddScoped<IProvinciaRepository,ProvinciaRepository >();
            services.AddScoped<IUbigeoRepository, UbigeoRepository>();

            services.AddScoped<IPersonaNaturalRepository,PersonaNaturalRepository >();
            services.AddScoped<IPersonaRepository, PersonaRepository>();

            services.AddScoped<AdministrativoUnitOfWork>();
            services.AddScoped<ComiteUnitOfWork>();
            services.AddScoped<MaestroUnitOfWork>();
            services.AddScoped<PersonaUnitOfWork>();


            services.AddDbContext<BDPVLContext>(opt =>
            {
                opt.UseSqlServer(options.ConnectionString);
            });

            return services;
        }

      

        public static IServiceCollection AddRepositoriesSp(this IServiceCollection services, Action<RepositoriesOptions> configureOptions)
        {
            //var options = new RepositoriesOptions();
            //configureOptions(options);

            //services.AddScoped<IPedidoRepository, PedidoRepository>();
            //services.AddScoped<IPersonaRepository, PersonaRepository>();
            //services.AddScoped<ITipoCambioRepository, TipoCambioRepository>();
            //services.AddScoped<IDapper, DataBase>();
            //services.AddScoped<PedidoUnitOfWork>();

            //services.AddDbContext<DbPedidoContext>(opt =>
            //{
            //    opt.UseSqlServer(options.ConnectionString);

            //});

            return services;
        }
        public static IServiceCollection AddManager(this IServiceCollection services)
        {

            services.AddScoped<IAplicationConstants, AplicationConstants>();
            services.AddScoped<IComiteAdminManager, ComiteAdminManager>();
            services.AddScoped<IMaestroManager, MaestroManager>();
            //services.AddScoped<IReporteManager, ReporteManager>();
            //services.AddScoped<IPedidoManager, PedidoManager>();

            return services;

        }


    }
}
