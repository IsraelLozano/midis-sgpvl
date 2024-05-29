using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using MIDIS.SGPVL.AppWeb.Injections;
using MIDIS.SGPVL.Contexto.See;
using MIDIS.SGPVL.Manager.MappingDto;
using MIDIS.SGPVL.Utils.Dtos;
using MIDIS.SGPVL.Utils.Filters;
using Newtonsoft.Json.Serialization;
using System.Globalization;
using System.Reflection;
using MIDIS.SGPVL.Utils.Helpers.FileManager;
using Serilog.Events;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation()
    .AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.AddMvc(options =>
{
    options.Filters.Add(new IgnoreAntiforgeryTokenAttribute());
});


builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("es-PE");
    options.SupportedCultures = new List<CultureInfo> { new CultureInfo("es-PE") };
    options.RequestCultureProviders.Clear();
});


//Configuracion ruta de los recursos-------------------------------------/
var recursos = configuration.GetSection("ResourceDto").Get<ResourceDto>(opt => opt.BindNonPublicProperties = true);
builder.Services.AddSingleton(recursos);
/*------------------------------------------------------------*/

//Configuracion para el BackEnd-------------------------------------/
BackEndConfig backEndConfig = configuration.GetSection("BackEndConfig").Get<BackEndConfig>();


//Auto Mapper
builder.Services.AddAutoMapper(typeof(AutoMapperHelper).GetTypeInfo().Assembly);

//EF Core - Inyeccion de Dependencia.
builder.Services.AddRepositories(opt => opt.ConnectionString = backEndConfig.BdSqlServer);
builder.Services.AddManager();
builder.Services.AddStorageManager(opt => opt.Type = StorageType.FileStorage);

//Servicio de acceso al contexto
builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(
    option =>
    {
        option.Cookie.Name = "Test.Session";
        option.IdleTimeout = TimeSpan.FromSeconds(1000);
        option.Cookie.HttpOnly = true;
        option.Cookie.IsEssential = true;
    }
    );

//Filters
builder.Services.AddScoped<ModelValidationAttribute>();
//Seri Log - Config -------------------------------------------/
var logger = new LoggerConfiguration()
                //.ReadFrom.Configuration(configuration.GetSection("Logging"))
    .WriteTo.File("SGPVL.txt", rollingInterval: RollingInterval.Day)
    //.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .CreateLogger();

builder.Host.UseSerilog(logger).ConfigureLogging(opt =>
{
    opt.ClearProviders();
    opt.SetMinimumLevel(LogLevel.Trace);
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

//Llama al migrate del seed.
//await app.MigrateDatabaseAsync();
app.UseEndpoints(endpoints =>
   {
       app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
   });
app.Run();
