using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MIDIS.SGPVL.AppWeb.Models;
using MIDIS.SGPVL.Manager.Settings;
using MIDIS.SGPVL.ManagerDto.Seguridad;

namespace MIDIS.SGPVL.AppWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAplicationConstants _aplicationConstants;

    public HomeController(ILogger<HomeController> logger, IAplicationConstants aplicationConstants)
    {
        _logger = logger;
        this._aplicationConstants = aplicationConstants;
    }

    public IActionResult Index()
    {
        UsuarioSesionBE userSesion = new UsuarioSesionBE(1, 1, 1, "ilozano", "Israel Lozano del Castillo", "", "Municipalidad Provincial del Rimac");

        _aplicationConstants.UsuarioSesionBE = userSesion;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
