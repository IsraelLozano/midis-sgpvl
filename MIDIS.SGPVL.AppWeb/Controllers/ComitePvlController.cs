using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MIDIS.SGPVL.Manager.ComitePvl;
using MIDIS.SGPVL.Manager.Maestro;
using MIDIS.SGPVL.Manager.Settings;
using MIDIS.SGPVL.ManagerDto.ComiteAdmin.Cmd;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Cmd;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Get;
using MIDIS.SGPVL.ManagerDto.Maestro.Get;
using MIDIS.SGPVL.Utils.Dtos;
using MIDIS.SGPVL.Utils.Enumerados;
using MIDIS.SGPVL.Utils.Helpers.FileManager;

namespace MIDIS.SGPVL.AppWeb.Controllers
{
    public class ComitePvlController : Controller
    {
        private readonly ILogger<ComiteAdminController> _logger;
        private readonly IMaestroManager _maestrasManager;
        private readonly IAplicationConstants _aplicationConstants;
        private readonly IComitePvlManager _comitePvlManager;
        private readonly IStorageManager _storageManager;
        private readonly ResourceDto _resourceDto;

        public ComitePvlController(ILogger<ComiteAdminController> logger,
            IMaestroManager maestrasManager,
            IAplicationConstants aplicationConstants,
            IStorageManager storageManager,
            ResourceDto resourceDto,
            IComitePvlManager comitePvlManager)
        {
            _logger = logger;
            _maestrasManager = maestrasManager;
            _aplicationConstants = aplicationConstants;
            _storageManager = storageManager;
            _resourceDto = resourceDto;
            _comitePvlManager = comitePvlManager;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new GetComiteParams();
            var _listDpto = await _maestrasManager.GetAllDptoAsync();
            ViewBag.dpto = new SelectList(_listDpto, nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.idDptoSelect);
            return View(vm);
        }

        [HttpPost]
        public async Task<JsonResult> IndexData(GetComiteParams param)
        {
            var data = await _comitePvlManager.GetComiteList(param);
            return Json(new
            {
                draw = param.draw,
                recordsFiltered = data.Count,
                recordsTotal = data.Count,
                data = data
            });
        }

        public async Task<IActionResult> addComite([FromQuery] int? id = 0)
        {
            var vm = new CmdComiteDto();

            if (id > 0)
            {
                vm = await _comitePvlManager.GetComiteByIdAsync(id.Value);
            }
            List<EnumeradoCabecera> listaMaestra = new List<EnumeradoCabecera>();
            listaMaestra.Add(EnumeradoCabecera.TIPO_OSB);
            listaMaestra.Add(EnumeradoCabecera.TIPO_ALIMENTO);

            var combos = await _maestrasManager.GetListEnumeradoByGrupo(listaMaestra);
            var tipoOsb = combos[EnumeradoCabecera.TIPO_OSB].ToList();
            var tipoAlimento = combos[EnumeradoCabecera.TIPO_ALIMENTO].ToList();
            ViewBag.tipoOsb = new SelectList(tipoOsb, nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.iTipOsb);
            ViewBag.tipoOsb = new SelectList(tipoAlimento, nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.iTipAlimento);

            return PartialView("_addModalComitePvl", vm);
        }

    }
}
