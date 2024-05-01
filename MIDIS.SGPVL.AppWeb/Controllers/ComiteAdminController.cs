using Microsoft.AspNetCore.Mvc;
using MIDIS.SGPVL.AppWeb.Models;
using MIDIS.SGPVL.Manager.ComiteAdmin;
using MIDIS.SGPVL.Manager.Maestro;
using MIDIS.SGPVL.Manager.Settings;

namespace MIDIS.SGPVL.AppWeb.Controllers
{
    public class ComiteAdminController : Controller
    {
        private readonly ILogger<ComiteAdminController> _logger;
        private readonly IMaestroManager _maestrasManager;
        private readonly IAplicationConstants _aplicationConstants;
        private readonly IComiteAdminManager _comiteAdminManager;

        public ComiteAdminController(ILogger<ComiteAdminController> logger, IMaestroManager maestrasManager, IAplicationConstants aplicationConstants, IComiteAdminManager comiteAdminManager)
        {
            _logger = logger;
            _maestrasManager = maestrasManager;
            _aplicationConstants = aplicationConstants;
            _comiteAdminManager = comiteAdminManager;
        }

        public async Task<IActionResult> Index()
        {
            var combos = await _comiteAdminManager.GetAdministrativoFilters("");

            ViewBag.dptos = combos.dptos;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> IndexData(dataParams param)
        {
            var data = await _comiteAdminManager.GetAdministrativo("");

            return Json(new
            {
                draw = param.draw,
                recordsFiltered = data.Count,
                recordsTotal = data.Count,
                data = data
            });
        }


    }
}
