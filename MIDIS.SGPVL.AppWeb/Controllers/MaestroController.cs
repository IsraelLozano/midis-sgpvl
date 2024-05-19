using Microsoft.AspNetCore.Mvc;
using MIDIS.SGPVL.Manager.Maestro;

namespace MIDIS.SGPVL.AppWeb.Controllers
{
    public class MaestroController : Controller
    {
        private readonly ILogger<MaestroController> _logger;
        private readonly IMaestroManager _maestrasManager;

        public MaestroController(ILogger<MaestroController> logger, IMaestroManager maestrasManager)
        {
            _logger = logger;
            _maestrasManager = maestrasManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Maestro/GetUbigeoByProvAsync/{id}")]
        public async Task<IActionResult> GetUbigeoByProvAsync(string id)
        {
            var resp = await _maestrasManager.getUbigeosByProvincia(id);
            return Json(resp);
        }

        [Route("Maestro/getProvinciaByDpto/{id}")]
        public async Task<IActionResult> getProvinciaByDpto(string id)
        {
            var resp = await _maestrasManager.getProvinciaByDpto(id);
            return Json(resp);
        }

        [Route("Maestro/getDistritoByDptoProv/{dpto}/{prov}")]
        public async Task<IActionResult> getDistritoByDptoProv(string dpto, string prov)
        {
            var resp = await _maestrasManager.getDistritoByDptoProv(dpto, prov);
            return Json(resp);
        }
        
        [Route("Maestro/getCentroPobladoByDistrito/{codDistrito}")]
        public async Task<IActionResult> getCentroPobladoByDistrito(string codDistrito)
        {
            var resp = await _maestrasManager.getCentroPobladoByDistrito(codDistrito);
            return Json(resp);
        }
    }
}
