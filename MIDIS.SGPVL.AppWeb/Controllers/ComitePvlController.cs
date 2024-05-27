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
using System;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private readonly IJuntaDirectivaManager _juntaDirectivaManager;
        private readonly ISocioManager _socioManager;
        private readonly IBeneficiarioManager _beneficiarioManager;

        public ComitePvlController(ILogger<ComiteAdminController> logger,
            IMaestroManager maestrasManager,
            IAplicationConstants aplicationConstants,
            IStorageManager storageManager,
            ResourceDto resourceDto,
            IComitePvlManager comitePvlManager,
            IJuntaDirectivaManager juntaDirectivaManager,
            ISocioManager socioManager,
            IBeneficiarioManager beneficiarioManager)
        {
            _logger = logger;
            _maestrasManager = maestrasManager;
            _aplicationConstants = aplicationConstants;
            _storageManager = storageManager;
            _resourceDto = resourceDto;
            _comitePvlManager = comitePvlManager;
            _juntaDirectivaManager = juntaDirectivaManager;
            _socioManager = socioManager;
            _beneficiarioManager = beneficiarioManager;
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
            var _tipoOsb = combos[EnumeradoCabecera.TIPO_OSB].ToList();
            var _tipoAlimento = combos[EnumeradoCabecera.TIPO_ALIMENTO].ToList();
            ViewBag.tipoOsb = new SelectList(_tipoOsb, nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.iTipOsb);
            ViewBag.tipoAlimento = new SelectList(_tipoAlimento, nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.iTipAlimento);

            return PartialView("_addModalComitePvl", vm);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAdmin(CmdComiteDto data)
        {
            if (ModelState.IsValid)
            {
                var resp = await _comitePvlManager.AddComitePvl(data);
                return Ok(resp.iCodComVasLeche > 0);
            }

            List<EnumeradoCabecera> listaMaestra = new List<EnumeradoCabecera>();
            listaMaestra.Add(EnumeradoCabecera.TIPO_OSB);
            listaMaestra.Add(EnumeradoCabecera.TIPO_ALIMENTO);

            var combos = await _maestrasManager.GetListEnumeradoByGrupo(listaMaestra);
            var _tipoOsb = combos[EnumeradoCabecera.TIPO_OSB].ToList();
            var _tipoAlimento = combos[EnumeradoCabecera.TIPO_ALIMENTO].ToList();
            ViewBag.tipoOsb = new SelectList(_tipoOsb, nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), data.iTipOsb);
            ViewBag.tipoAlimento = new SelectList(_tipoAlimento, nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), data.iTipAlimento);

            return PartialView("_addModalComitePvl", data);
        }


        #region Junta Directiva

        [Route("ComitePvl/GetLisJuntaDirectivaByIdPvlAsync/{id}")]
        public async Task<IActionResult> GetLisJuntaDirectivaByIdPvlAsync(int id)
        {
            var query = await _juntaDirectivaManager.GetJuntaDirectivaByComiteAsync(id);
            return Ok(query);
        }

        [Route("ComitePvl/GetJuntaDirectivaAsync/{id}")]
        public async Task<IActionResult> GetJuntaDirectivaAsync([FromQuery] int? id = 0)
        {
            var vm = new CmdComiteJdDto();

            if (id.Value > 0)
            {
                vm = await _juntaDirectivaManager.GetJuntaByIdAsync(id.Value);

            }
            List<EnumeradoCabecera> listaMaestra = new List<EnumeradoCabecera>();
            listaMaestra.Add(EnumeradoCabecera.TIPO_RESOLUCION);

            var combos = await _maestrasManager.GetListEnumeradoByGrupo(listaMaestra);
            var tipoResolucion = combos[EnumeradoCabecera.TIPO_RESOLUCION].ToList();
            ViewBag.tipoResol = new SelectList(tipoResolucion, nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.iTipResolucion);

            return PartialView("_addJuntaDirectiva", vm);
        }

        [HttpPost("ComitePvl/SaveJuntaDirectivaAsync")]
        public async Task<IActionResult> SaveJuntaDirectivaAsync(CmdComiteJdDto data)
        {

            if (ModelState.IsValid)
            {
                var resp = await _juntaDirectivaManager.AddComiteAdmin(data);
                return Ok(resp.iIdJunta > 0);
            }
            List<EnumeradoCabecera> listaMaestra = new List<EnumeradoCabecera>();
            listaMaestra.Add(EnumeradoCabecera.TIPO_RESOLUCION);

            var combos = await _maestrasManager.GetListEnumeradoByGrupo(listaMaestra);
            var tipoResolucion = combos[EnumeradoCabecera.TIPO_RESOLUCION].ToList();
            ViewBag.tipoResol = new SelectList(tipoResolucion, nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), data.iTipResolucion);

            return PartialView("_addJuntaDirectiva", data);
        }

        #endregion

        #region Miembro - Junta Directiva

        [HttpPost("ComitePvl/SaveMiembroJunta")]
        public async Task<IActionResult> SaveMiembroJunta(CmdMiembroJdDto data)
        {
            if (ModelState.IsValid)
            {
                await _juntaDirectivaManager.AddComiteMemberAdmin(data);
                var miembros = await _juntaDirectivaManager.GetMiembrosByJuntaAsync(data.iIdJunta);

                return Ok(miembros);
            }

            await getComboMemberAsync(data);
            return PartialView("_addMiembroJuntaDirectiva", data);
        }

        [Route("ComitePvl/GetMiembroJunta/{id}")]
        public async Task<IActionResult> GetMiembroJunta(int? id = 0)
        {
            var vm = new CmdMiembroJdDto();

            if (id.Value > 0)
            {
                vm = await _juntaDirectivaManager.GetMiembroJuntaByIdAsync(id.Value);

            }

            await getComboMemberAsync(vm);
            List<EnumeradoCabecera> listaMaestra = new List<EnumeradoCabecera>();
            listaMaestra.Add(EnumeradoCabecera.TIPO_CARGOS);

            var combos = await _maestrasManager.GetListEnumeradoByGrupo(listaMaestra);
            ViewBag.cargos = new SelectList(combos[EnumeradoCabecera.TIPO_CARGOS].ToList(), nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.iTipCargo);

            return PartialView("_addMiembroJuntaDirectiva", vm);
        }

        [NonAction]
        public async Task getComboMemberAsync(CmdMiembroJdDto vm)
        {
            List<EnumeradoCabecera> listaMaestra = new List<EnumeradoCabecera>();
            listaMaestra.Add(EnumeradoCabecera.TIPO_CARGO_JD);
            listaMaestra.Add(EnumeradoCabecera.TIPO_DOCUMENTO);
            listaMaestra.Add(EnumeradoCabecera.GENERO);

            var combos = await _maestrasManager.GetListEnumeradoByGrupo(listaMaestra);

            ViewBag.tipoCargo = new SelectList(combos[EnumeradoCabecera.TIPO_CARGO_JD].ToList(), nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.iTipCargo);
            ViewBag.tipoDocumentos = new SelectList(combos[EnumeradoCabecera.TIPO_DOCUMENTO].ToList(), nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.iTipDocumento);
            ViewBag.generos = new SelectList(combos[EnumeradoCabecera.GENERO].ToList(), nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.bSexo);
        }

        #endregion

        #region Socio - Junta Directiva

        [Route("ComitePvl/GetLisSocioByIdPvlAsync/{id}")]
        public async Task<IActionResult> GetLisSocioByIdPvlAsync(int id)
        {
            var query = await _socioManager.GetListSocioByComiteAsync(id);
            return Ok(query);
        }


        [HttpPost("ComitePvl/SaveSocioJunta")]
        public async Task<IActionResult> SaveSocioJunta(CmdSocioDto data)
        {
            if (ModelState.IsValid)
            {
                await _socioManager.AddSocioAsync(data);
                var miembros = await _socioManager.GetListSocioByComiteAsync(data.iCodComVasLeche);

                return Ok(miembros);
            }

            await getComboSocioAsync(data);
            return PartialView("_addSocioJuntaDirectiva", data);
        }

        [Route("ComitePvl/GetSocioJunta/{id}")]
        public async Task<IActionResult> GetSocioJunta(int? id = 0)
        {
            var vm = new CmdSocioDto();

            if (id.Value > 0)
            {
                vm = await _socioManager.GetSocioByIdAsync(id.Value);

            }

            await getComboSocioAsync(vm);
            return PartialView("_addSocioJuntaDirectiva", vm);
        }

        [NonAction]
        public async Task getComboSocioAsync(CmdSocioDto vm)
        {
            List<EnumeradoCabecera> listaMaestra = new List<EnumeradoCabecera>();
            listaMaestra.Add(EnumeradoCabecera.TIPO_SOCIO);
            listaMaestra.Add(EnumeradoCabecera.TIPO_DOCUMENTO);
            listaMaestra.Add(EnumeradoCabecera.GENERO);

            var combos = await _maestrasManager.GetListEnumeradoByGrupo(listaMaestra);

            ViewBag.tipoSocio = new SelectList(combos[EnumeradoCabecera.TIPO_SOCIO].ToList(), nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.iTipSocio);
            ViewBag.tipoDocumentos = new SelectList(combos[EnumeradoCabecera.TIPO_DOCUMENTO].ToList(), nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.iTipDocumento);
            ViewBag.generos = new SelectList(combos[EnumeradoCabecera.GENERO].ToList(), nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.bSexo);
        }

        #endregion


        #region Beneficiario - Junta Directiva

        [Route("ComitePvl/GetLisBeneficiarioByIdPvlAsync/{id}")]
        public async Task<IActionResult> GetListBeneficiarioByIdPvlAsync(int id)
        {
            var query = await _beneficiarioManager.GetListBeneficiarioByComiteAsync(id);
            return Ok(query);
        }


        [HttpPost("ComitePvl/SaveBeneficiarioJunta")]
        public async Task<IActionResult> SaveBeneficiarioJunta(CmdBeneficiarioDto data)
        {
            if (ModelState.IsValid)
            {
                await _beneficiarioManager.AddBeneficiarioAsync(data);
                var miembros = await _beneficiarioManager.GetListBeneficiarioByComiteAsync(data.iCodComVasLeche);
                return Ok(miembros);
            }

            await getComboBeneficiarioAsync(data);
            return PartialView("_addBeneficiarioJuntaDirectiva", data);
        }

        [Route("ComitePvl/GetBeneficiarioJunta/{id}/{idUbigeo}")]
        public async Task<IActionResult> GetBeneficiarioJunta(int? id = 0, string idUbigeo = "")
        {
            var vm = new CmdBeneficiarioDto();

            if (id.Value > 0)
            {
                vm = await _beneficiarioManager.GetBeneficiarioByIdAsync(id.Value);
            }
            var socios = (await _socioManager.GetListSocioByUbigeo(idUbigeo)).Select(l => new
            {
                id = l.iIdSocio,
                fullName = l.nombreFull()
            });
            ViewBag.listSocios = new SelectList(socios,"id" , "fullName", vm.iIdSocio);

            await getComboBeneficiarioAsync(vm);
            return PartialView("_addBeneficiarioJuntaDirectiva", vm);
        }

        [NonAction]
        public async Task getComboBeneficiarioAsync(CmdBeneficiarioDto vm)
        {
            List<EnumeradoCabecera> listaMaestra = new List<EnumeradoCabecera>();
            listaMaestra.Add(EnumeradoCabecera.TIPO_CLASIFICACION_SOCIO_ECONOMICA);
            listaMaestra.Add(EnumeradoCabecera.TIPO_DOCUMENTO);
            listaMaestra.Add(EnumeradoCabecera.GENERO);

            var combos = await _maestrasManager.GetListEnumeradoByGrupo(listaMaestra);

            ViewBag.tipoDocumentos = new SelectList(combos[EnumeradoCabecera.TIPO_DOCUMENTO].ToList(), nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.iTipDocumento);
            ViewBag.claEcono = new SelectList(combos[EnumeradoCabecera.TIPO_CLASIFICACION_SOCIO_ECONOMICA].ToList(), nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.vClaSocEconomica);
            ViewBag.generos = new SelectList(combos[EnumeradoCabecera.GENERO].ToList(), nameof(GetEnumeradoComboDto.id), nameof(GetEnumeradoComboDto.descripcion), vm.bSexo);
        }

        #endregion

        #region download - file

        [Route("ComitePvl/downloadResolucionAsync/{id}")]
        public async Task<IActionResult> downloadResolucionAsync(int id)
        {
            try
            {
                var response = await _juntaDirectivaManager.GetJuntaByIdAsync(id);

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
        #endregion

        #region Reportes

        [HttpGet, DisableRequestSizeLimit]
        [Route("ComitePvl/GetExcelJuntaDirectivaAsync/{codUbigeo}")]
        public async Task<IActionResult> GetExcelJuntaDirectivaAsync(string codUbigeo)
        {
            try
            {
                var query = await _comitePvlManager.GetExcelJuntaDirectivaAsync(codUbigeo);
                var fileName = string.Format("GetExcelJuntaDirectivaAsync{0}.xlsx", DateTime.Now.ToString("yyyy_MM_dd_hh_mm"));

                return File(query, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
            }
        }
        
        [HttpGet, DisableRequestSizeLimit]
        [Route("ComitePvl/GetExcelSociosAsync/{codUbigeo}")]
        public async Task<IActionResult> GetExcelSociosAsync(string codUbigeo)
        {
            try
            {
                var query = await _comitePvlManager.GetExcelSocioAsync(codUbigeo);
                var fileName = string.Format("GetExcelSociosAsync{0}.xlsx", DateTime.Now.ToString("yyyy_MM_dd_hh_mm"));

                return File(query, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
            }
        }
        
        [HttpGet, DisableRequestSizeLimit]
        [Route("ComitePvl/GetExcelBeneficiariosAsync/{codUbigeo}")]
        public async Task<IActionResult> GetExcelBeneficiariosAsync(string codUbigeo)
        {
            try
            {
                var query = await _comitePvlManager.GetExcelUsuariosAsync(codUbigeo);
                var fileName = string.Format("GetExcelBeneficiariosAsync{0}.xlsx", DateTime.Now.ToString("yyyy_MM_dd_hh_mm"));

                return File(query, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
            }
        }

       

        #endregion

    }
}
