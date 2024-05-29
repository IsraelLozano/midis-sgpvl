using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MIDIS.SGPVL.AppWeb.Models;
using MIDIS.SGPVL.Manager.ComiteAdmin;
using MIDIS.SGPVL.Manager.Maestro;
using MIDIS.SGPVL.Manager.Settings;
using MIDIS.SGPVL.ManagerDto.ComiteAdmin.Cmd;
using MIDIS.SGPVL.ManagerDto.Maestro.Get;
using MIDIS.SGPVL.Utils.Dtos;
using MIDIS.SGPVL.Utils.Enumerados;
using MIDIS.SGPVL.Utils.Helpers.FileManager;

namespace MIDIS.SGPVL.AppWeb.Controllers
{
    public class ComiteAdminController : Controller
    {
        private readonly ILogger<ComiteAdminController> _logger;
        private readonly IMaestroManager _maestrasManager;
        private readonly IAplicationConstants _aplicationConstants;
        private readonly IComiteAdminManager _comiteAdminManager;
        private readonly IStorageManager _storageManager;
        private readonly ResourceDto _resourceDto;

        public ComiteAdminController(
            ILogger<ComiteAdminController> logger,
            IMaestroManager maestrasManager,
            IAplicationConstants aplicationConstants,
            IComiteAdminManager comiteAdminManager,
            IStorageManager storageManager,
            ResourceDto resourceDto)
        {
            _logger = logger;
            _maestrasManager = maestrasManager;
            _aplicationConstants = aplicationConstants;
            _comiteAdminManager = comiteAdminManager;
            _storageManager = storageManager;
            _resourceDto = resourceDto;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Ingresando a ===> GetAdministrativoFilters()");
                var combos = await _comiteAdminManager.GetAdministrativoFilters("");
                _logger.LogInformation("Iniciando la pagina Index de (ComiteAdminController)");
                ViewBag.dptos = combos.dptos;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                _logger.LogInformation(ex.Message, ex);
                throw ex;
            }
            
        }

        [HttpPost]
        public async Task<JsonResult> IndexData(GetAdminParams param)
        {
            var data = await _comiteAdminManager.GetAdministrativo(param);

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
            var vm = new CmdComiteAdminDto();

            if (id > 0)
            {
                vm = await _comiteAdminManager.GetAdministrativoByIdAsync(id.Value);
            }
            List<EnumeradoCabecera> listaMaestra = new List<EnumeradoCabecera>();
            listaMaestra.Add(EnumeradoCabecera.TIPO_RESOLUCION);

            var combos = await _maestrasManager.GetListEnumeradoByGrupo(listaMaestra);
            var tipoResolucion = combos[EnumeradoCabecera.TIPO_RESOLUCION].ToList();
            ViewBag.tipoResol = new SelectList(tipoResolucion, nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.iTipResolucion);

            return PartialView("_addModalComite", vm);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAdmin(CmdComiteAdminDto data)
        {
            if (ModelState.IsValid)
            {
                var resp = await _comiteAdminManager.AddComiteAdmin(data);
                return Ok(resp.iIdComite > 0);
            }

            List<EnumeradoCabecera> listaMaestra = new List<EnumeradoCabecera>();
            listaMaestra.Add(EnumeradoCabecera.TIPO_RESOLUCION);

            var combos = await _maestrasManager.GetListEnumeradoByGrupo(listaMaestra);
            var tipoResolucion = combos[EnumeradoCabecera.TIPO_RESOLUCION].ToList();
            ViewBag.tipoResol = new SelectList(tipoResolucion, nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), data.iTipResolucion);
            return PartialView("_addModalComite", data);
        }

        [Route("ComiteAdmin/downloadResolucionAsync/{id}")]
        public async Task<IActionResult> downloadResolucionAsync(int id)
        {
            try
            {
                var response = await _comiteAdminManager.GetAdministrativoByIdAsync(id);

                if (response == null)
                {
                    return NotFound();
                }
                var fullPath = Path.Combine(_resourceDto.Documents, response.vNomArcGuid);
                byte[] fileBytes = _storageManager.GetBytes(fullPath);
                return File(fileBytes, "application/pdf", response.vNomArchivo);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
                throw ex;
            }
        }

        #region Miembros

        [HttpPost]
        public async Task<IActionResult> SaveMemberAdmin([FromBody] CmdComiteMemberAdminDto data)
        {
            if (ModelState.IsValid)
            {
                var resp = await _comiteAdminManager.AddComiteMemberAdmin(data);
                var miembros = await _comiteAdminManager.GetMiembroByIdComiteAsync(data.iIdComite);

                return Ok(miembros);
            }

            await getComboMemberAsync(data);
            return PartialView("_addModalComite", data);
        }

        public async Task<IActionResult> addMemberModal(int idComite)
        {
            var vm = new CmdComiteMemberAdminDto();
            var infoAdicional = await _comiteAdminManager.GetAdministrativoByIdAsync(idComite);
            await getComboMemberAsync(vm);
            vm.iIdComite = idComite;
            vm.tipoResolucionTexto = $"{infoAdicional.tipoResolucionText}: {infoAdicional.vNumResolucion}";
            vm.UbigeoCompletoFull = infoAdicional.ubigeoFull;
            return PartialView("_addMemberAdmin", vm);
        }

        [Route("ComiteAdmin/member/{id}")]
        public async Task<IActionResult> ViewMember(int id)
        {
            ViewBag.tag = false;
            var miembros = _comiteAdminManager.GetMiembroByIdComiteAsync(id);
            return View();
        }

        [NonAction]
        public async Task getComboMemberAsync(CmdComiteMemberAdminDto vm)
        {
            List<EnumeradoCabecera> listaMaestra = new List<EnumeradoCabecera>();
            listaMaestra.Add(EnumeradoCabecera.TIPO_CARGOS);
            listaMaestra.Add(EnumeradoCabecera.TIPO_DOCUMENTO);
            listaMaestra.Add(EnumeradoCabecera.GENERO);

            var combos = await _maestrasManager.GetListEnumeradoByGrupo(listaMaestra);

            var cargos = combos[EnumeradoCabecera.TIPO_CARGOS].ToList();
            var documentos = combos[EnumeradoCabecera.TIPO_DOCUMENTO].ToList();
            var sexo = combos[EnumeradoCabecera.GENERO].ToList();

            ViewBag.tipoCargo = new SelectList(cargos, nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.iTipCargo);
            ViewBag.tipoDocumentos = new SelectList(documentos, nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.iTipDocumento);
            ViewBag.generos = new SelectList(sexo, nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.bSexo);
        }

        #endregion

        #region Reportes

        [HttpGet, DisableRequestSizeLimit]
        [Route("ComiteAdmin/GetExcelComiteAdministrativoAsync/{codUbigeo}")]
        public async Task<IActionResult> GetExcelComiteAdministrativoAsync(string codUbigeo)
        {
            try
            {
                var query = await _comiteAdminManager.GetExcelComiteAdministrativoAsync(codUbigeo);
                var fileName = string.Format("GetExcelComiteAdministrativoAsync{0}.xlsx", DateTime.Now.ToString("yyyy_MM_dd_hh_mm"));

                return File(query, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
            }
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("ComiteAdmin/GetExcelComiteMembersAdminiAsync/{codUbigeo}")]
        public async Task<IActionResult> GetExcelComiteMembersAdminiAsync(string codUbigeo)
        {
            var query = await _comiteAdminManager.GetExcelComiteMembersAdminiAsync(codUbigeo);
            var fileName = string.Format("GetExcelComiteMembersAdminiAsync{0}.xlsx", DateTime.Now.ToString("yyyy_MM_dd_hh_mm"));

            return File(query, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

        }

        #endregion
    }
}
