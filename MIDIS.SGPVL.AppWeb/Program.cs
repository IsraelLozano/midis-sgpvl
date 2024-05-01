using Microsoft.AspNetCore.Http.Features;
using MIDIS.SGPVL.Manager.MappingDto;
using MIDIS.SGPVL.Utils.Dtos;
using MIDIS.SGPVL.Utils.Filters;
using MIDIS.SGPVL.AppWeb.Injections;
using System.Reflection;
using MIDIS.SGPVL.Contexto.See;
using System.Security.Principal;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
})
.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
})
.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

var configuration = builder.Configuration;

//Configuracion de correo-------------------------------------/
var emailConfig = configuration.GetSection("MailSettings").Get<MailSettings>(opt => opt.BindNonPublicProperties = true);
builder.Services.AddSingleton(emailConfig);
builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

/*------------------------------------------------------------*/

//inyeccion de dependencia para poder usar las sesiones
//builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddTransient<IPrincipal>(
//    provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);


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
//builder.Services.AddTransient<IEmailSender, EmailSender>();
//builder.Services.AddStorageManager(opt => opt.Type = StorageType.FileStorage);

//Servicio de acceso al contexto
builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(
    option =>
    {
        option.Cookie.Name = "Test.Session";
        option.IdleTimeout = TimeSpan.FromSeconds(600);
        option.Cookie.HttpOnly = true;
        option.Cookie.IsEssential = true;
    }
    );

//Filters
builder.Services.AddScoped<ModelValidationAttribute>();

//builder.Services.AddControllersWithViews()


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

//Llama al migrate del seed.
//await app.MigrateDatabaseAsync();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
