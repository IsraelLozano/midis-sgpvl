
using AutoMapper;
using Microsoft.AspNetCore.Http;
using MIDIS.SGPVL.AppWeb.Models;
using MIDIS.SGPVL.Entity.Models.Comite;
using MIDIS.SGPVL.Entity.Models.Persona;
using MIDIS.SGPVL.Manager.Maestro;
using MIDIS.SGPVL.Manager.Settings;
using MIDIS.SGPVL.ManagerDto.ComiteAdmin.Cmd;
using MIDIS.SGPVL.ManagerDto.ComiteAdmin.Get;
using MIDIS.SGPVL.ManagerDto.Maestro.Get;
using MIDIS.SGPVL.Repository.UnitOfWork;
using MIDIS.SGPVL.Utils.Dtos;
using MIDIS.SGPVL.Utils.Enumerados;
using MIDIS.SGPVL.Utils.Helpers.FileManager;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MIDIS.SGPVL.Manager.ComiteAdmin
{
    public class ComiteAdminManager : IComiteAdminManager
    {

        private readonly IMapper _mapper;
        private readonly MaestroUnitOfWork _maestraUnitOfWork;
        private readonly IMaestroManager _maestraManager;
        private readonly PersonaUnitOfWork _personaUnitOfWork;
        private readonly AdministrativoUnitOfWork _administrativoUnitOfWork;
        private readonly ResourceDto _resourceDto;
        private readonly IAplicationConstants _aplicationConstants;
        private readonly IStorageManager _storageManager;

        public ComiteAdminManager(IMapper mapper,
            MaestroUnitOfWork maestraUnitOfWork,
            IMaestroManager maestraManager,
            PersonaUnitOfWork personaUnitOfWork,
            AdministrativoUnitOfWork administrativoUnitOfWork,
            ResourceDto resourceDto,
            IAplicationConstants aplicationConstants,
            IStorageManager storageManager)
        {
            _mapper = mapper;
            _maestraUnitOfWork = maestraUnitOfWork;
            this._maestraManager = maestraManager;
            _personaUnitOfWork = personaUnitOfWork;
            _administrativoUnitOfWork = administrativoUnitOfWork;
            this._resourceDto = resourceDto;
            this._aplicationConstants = aplicationConstants;
            _storageManager = storageManager;
        }
        public async Task<List<GetAdministrativoDto>> GetAdministrativo(GetAdminParams param)
        {
            var ubigeo = string.IsNullOrEmpty(param.idUbigeo) ? $"{param.idDptoSelect}{param.idProvSelect}" : param.idUbigeo;
            var filtro = param.search?.value ?? "";

            var query = new List<VLAdministrativo>();

            if (!string.IsNullOrEmpty(param.idUbigeo))
            {
                query = _administrativoUnitOfWork
                ._comiteAdminRepository
                .GetAll(l => l.vUbigeo == param.idUbigeo
                && (l.vNumResolucion.Contains(filtro) || filtro == ""),
                includeProperties: "iTipResolucionNavigation,VLAdmMiembros.iCodPersonaNavigation.iTipDocumentoNavigation,VLAdmMiembros.iTipCargoNavigation");
            }
            else
            {
                query = _administrativoUnitOfWork
                ._comiteAdminRepository
                .GetAll(l => l.vUbigeo.StartsWith(ubigeo)
                && (l.vNumResolucion.Contains(filtro) || filtro == ""),
                includeProperties: "iTipResolucionNavigation,VLAdmMiembros.iCodPersonaNavigation.iTipDocumentoNavigation,VLAdmMiembros.iTipCargoNavigation");
            }


            var ubigeos = query.Select(s => s.vUbigeo).Distinct().ToList();
            var listUbigeos = await _maestraManager.getDistritoFull(ubigeos);

            var response = _mapper.Map<List<GetAdministrativoDto>>(query);

            response.ForEach(u =>
            {
                var fullUbigeo = listUbigeos.FirstOrDefault(l => l.codigo == u.vUbigeo);
                u.ubigeoFull = fullUbigeo.full();
            });
            return response;
        }
        public async Task<GetAdministrativoFiltersDto> GetAdministrativoFilters(string filter)
        {
            var response = new GetAdministrativoFiltersDto();
            response.dptos = _mapper.Map<List<GetDptoDto>>(_maestraUnitOfWork
                ._departamentoRepository
                .GetAll(l => l.vCodigoDepartamento != "00", orderBy: l => l.OrderBy(s => s.vDescripcion)));
            return response;
        }
        public async Task<CmdComiteAdminDto> GetAdministrativoByIdAsync(int id)
        {
            var query = _administrativoUnitOfWork._comiteAdminRepository.GetAll(l => l.iIdComite == id, includeProperties: "iTipResolucionNavigation");
            var ubigeos = query.Select(s => s.vUbigeo).Distinct().ToList();
            var listUbigeos = await _maestraManager.getDistritoFull(ubigeos);
            var response = _mapper.Map<CmdComiteAdminDto>(query.FirstOrDefault());
            response.ubigeoFull = listUbigeos.FirstOrDefault().full();
            response.tipoResolucionText = $"{query.FirstOrDefault().iTipResolucionNavigation.vDescripcion}: {response.vNumResolucion}";
            return response;


        }
        public async Task<CmdComiteAdminDto> AddComiteAdmin(CmdComiteAdminDto model)
        {
            var entidad = _mapper.Map<VLAdministrativo>(model);
            try
            {
                var rutaCarpeta = $"{model.vUbigeo}-{model.nombreDistSelect}";
                var fileNameSave = string.Format("file_{0}.pdf", DateTime.Now.ToString("yyyyMMddTHHmmss"));

                if (entidad.iIdComite == 0)
                {
                    entidad.dFecRegistro = entidad.dFecModifica = DateTime.Now;
                    entidad.vUsuRegistro = entidad.vUsuModifica = _aplicationConstants.UsuarioSesionBE.Credenciales;
                    entidad.bVigente = false;
                    entidad.vNomArcGuid = Path.Combine(rutaCarpeta, fileNameSave);
                    entidad.iNumMiembro = 0;
                    //Guid.NewGuid().ToString();
                    entidad.vNomArchivo = model.FileResol.FileName;

                    _administrativoUnitOfWork._comiteAdminRepository.Insert(entidad);
                }
                else
                {
                    //var query = _administrativoUnitOfWork._comiteAdminRepository.GetById(model.iIdComite);
                    _administrativoUnitOfWork._comiteAdminRepository.Update(entidad);
                }
                var result = await _administrativoUnitOfWork.SaveAsync() != 0;

                if (model.iIdComite == 0)
                {
                    var pathToSave = Path.Combine(_resourceDto.Documents, rutaCarpeta);
                    await _storageManager.SaveFileFormCollection(pathToSave, fileNameSave, model.FileResol);
                    //await addMemberDefaults(entidad.iIdComite);
                }

                return _mapper.Map<CmdComiteAdminDto>(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //private async Task addMemberDefaults(int iIdComite)
        //{
        //    List<EnumeradoCabecera> listaMaestra = new List<EnumeradoCabecera>();
        //    listaMaestra.Add(EnumeradoCabecera.TIPO_CARGOS);

        //    var combos = await _maestraManager.GetListEnumeradoByGrupo(listaMaestra);
        //    var cargos= combos[EnumeradoCabecera.TIPO_CARGOS].ToList();

        //    var listMembers = cargos.Select(l=> new )
        //}

        #region Comite-Miembro

        public async Task<List<GetAdminMiembroDto>> GetMiembroByIdComiteAsync(int idAdmin)
        {
            var query = _administrativoUnitOfWork
                ._comiteAdminMiembroRepository
                .GetAll(l => l.iIdComite == idAdmin, includeProperties: "iTipCargoNavigation,iCodPersonaNavigation.iTipDocumentoNavigation");
            var resp = _mapper.Map<List<GetAdminMiembroDto>>(query);
            return resp;
        }
        public async Task<CmdComiteMemberAdminDto> AddComiteMemberAdmin(CmdComiteMemberAdminDto model)
        {
            var entidad = _mapper.Map<VLAdmMiembro>(model);
            try
            {
                if (entidad.iIdMiembro == 0)
                {
                    entidad.dFecRegistro = entidad.dFecModifica = DateTime.Now;
                    entidad.vUsuRegistro = entidad.vUsuModifica = _aplicationConstants.UsuarioSesionBE.Credenciales;
                    entidad.bActivo = true;

                    var persona = getPersona(model);

                    _personaUnitOfWork._personaRepository.Insert(persona);

                    await _personaUnitOfWork.SaveAsync();

                    entidad.iCodPersona = persona.iCodPersona;

                    _administrativoUnitOfWork._comiteAdminMiembroRepository.Insert(entidad);

                }
                else
                {
                    //_administrativoUnitOfWork._comiteAdminRepository.Update(entidad);
                }

                await _administrativoUnitOfWork.SaveAsync();

                //Actualizar nro Miembros 
                var comite = _administrativoUnitOfWork._comiteAdminRepository.GetById(model.iIdComite);

                int nroActivo = _administrativoUnitOfWork._comiteAdminMiembroRepository.GetAll(l => l.iIdComite == model.iIdComite && l.bActivo.Value).Count;

                comite.iNumMiembro = nroActivo;

                comite.bVigente = nroActivo >= 7;

                _administrativoUnitOfWork._comiteAdminRepository.Update(comite);

                await _administrativoUnitOfWork.SaveAsync();

                return _mapper.Map<CmdComiteMemberAdminDto>(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private VLPersona getPersona(CmdComiteMemberAdminDto model)
        {
            var persona = new VLPersona
            {
                iTipPersona = (int)EnumTipoPersona.natural,
                vNomComercial = $"{model.vApePaterno} {model.vApeMaterno}, {model.vNombre}",
                vNomCompleto = $"{model.vApePaterno} {model.vApeMaterno}, {model.vNombre}",
                vRuc = string.Empty,
                bEstado = true
            };
            persona.VLPerNatural = new VLPerNatural
            {
                vApePaterno = model.vApePaterno,
                vApeMaterno = model.vApeMaterno,
                vNombre = model.vNombre,
                vNroDocumento = model.vNroDocumento,
                bActivo = true,
                iTipDocumento = model.iTipDocumento,
                bSexo = model.bSexo,
                vTelFijo = model.vTelFijo,
                vCelular = model.vCelular,
                vEmail = model.vEmail,
                vDireccion = model.vDireccion,
                vUbigeo = string.Empty,
                dFecNacimiento = null
            };

            return persona;

        }

        #endregion

        #region Reporte - Excel

        public async Task<MemoryStream> GetExcelComiteAdministrativoAsync(string codUbigeo)
        {
            var param = new GetAdminParams
            {
                idDptoSelect = codUbigeo.Substring(0, 2),
                idProvSelect = codUbigeo.Length == 4 ? codUbigeo.Substring(0, 4) : null,
                idUbigeo = codUbigeo.Length == 6 ? codUbigeo.Substring(0, 6) : null
            };

            var data = await this.GetAdministrativo(param);
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("COMITE ADMIN");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                const int startRow = 5;
                var row = startRow;
                worksheet.View.ShowGridLines = false;

                worksheet.Cells["A1"].Value = "LISTADO DE COMITE ADMINISTRATIVO";
                using (var r = worksheet.Cells["A1:L1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Size = 15;
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["A4"].Value = "Codigo";
                worksheet.Cells["B4"].Value = "Ubigeo";
                worksheet.Cells["C4"].Value = "Dpto";
                worksheet.Cells["D4"].Value = "Provincia";
                worksheet.Cells["E4"].Value = "Distrito";
                worksheet.Cells["F4"].Value = "Tipo Resolucion";
                worksheet.Cells["G4"].Value = "Nro Resolucion";
                worksheet.Cells["H4"].Value = "Fecha Emision";
                worksheet.Cells["I4"].Value = "Fecha Inicio";
                worksheet.Cells["J4"].Value = "Fecha Fin";
                worksheet.Cells["K4"].Value = "Vigente";
                worksheet.Cells["L4"].Value = "Nro Miembros";
                worksheet.Cells["A4:L4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:L4"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:L4"].Style.Font.Bold = true;


                row = 5;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.iIdComite;
                    worksheet.Cells[row, 2].Value = item.vUbigeo;
                    worksheet.Cells[row, 3].Value = item.ubigeoFull.Split("/")[0];
                    worksheet.Cells[row, 4].Value = item.ubigeoFull.Split("/")[1];
                    worksheet.Cells[row, 5].Value = item.ubigeoFull.Split("/")[2];
                    worksheet.Cells[row, 6].Value = item.iTipResolucionNavigation.descripcion;
                    worksheet.Cells[row, 7].Value = item.vNumResolucion;
                    worksheet.Cells[row, 8].Value = item.dFecEmision.ToShortDateString();
                    worksheet.Cells[row, 9].Value = item.dFecInicio.ToShortDateString();
                    worksheet.Cells[row, 10].Value = item.dFecFin.ToShortDateString();
                    worksheet.Cells[row, 11].Value = item.bVigente.Value ? "SI" : "NO";
                    worksheet.Cells[row, 12].Value = item.VLAdmMiembros.Count();
                    row++;
                }

                var sRango = "A4:L" + (row - 1).ToString();
                worksheet.Cells[sRango].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells[sRango].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[sRango].AutoFitColumns();
                worksheet.Cells[sRango].Style.HorizontalAlignment = ExcelHorizontalAlignment.General;
                worksheet.Cells[sRango].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                //worksheet.Cells[sRango].Style.WrapText = true;

                xlPackage.Workbook.Properties.Title = "Data Relevante";
                xlPackage.Workbook.Properties.Author = "Israel Lozano del Castillo danielitolozano85@gmail.com";
                xlPackage.Workbook.Properties.Subject = "Data Relevante Empresarial";
                xlPackage.Save();
                // Response.Clear();

            }
            stream.Position = 0;
            return stream;
        }
        public async Task<MemoryStream> GetExcelComiteMembersAdminiAsync(string codUbigeo)
        {
            var data = _administrativoUnitOfWork
                    ._comiteAdminMiembroRepository
                    .GetAll(l => l.iIdComiteNavigation.vUbigeo.StartsWith(codUbigeo)
                    , includeProperties: "iTipCargoNavigation,iCodPersonaNavigation.iTipDocumentoNavigation,iIdComiteNavigation.iTipResolucionNavigation");

            var ubigeos = data.Select(s => s.iIdComiteNavigation.vUbigeo).Distinct().ToList();
            var listUbigeos = await _maestraManager.getDistritoFull(ubigeos);
            data.ForEach(u =>
            {
                var fullUbigeo = listUbigeos.FirstOrDefault(l => l.codigo == u.iIdComiteNavigation.vUbigeo);
                u.dpto = fullUbigeo.dpto;
                u.provincia = fullUbigeo.provincia;
                u.distrito = fullUbigeo.descripcion;
            });

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("COMITE ADMIN");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                const int startRow = 5;
                var row = startRow;
                worksheet.View.ShowGridLines = false;

                worksheet.Cells["A1"].Value = "LISTADO MIEMBROS COMITE ADMINISTRATIVO";
                using (var r = worksheet.Cells["A1:Q1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Size = 15;
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["A4"].Value = "Codigo";
                worksheet.Cells["B4"].Value = "Ubigeo";
                worksheet.Cells["C4"].Value = "Dpto";
                worksheet.Cells["D4"].Value = "Provincia";
                worksheet.Cells["E4"].Value = "Distrito";
                worksheet.Cells["F4"].Value = "Tipo Resolucion";
                worksheet.Cells["G4"].Value = "Nro Resolucion";
                worksheet.Cells["H4"].Value = "Cargo";
                worksheet.Cells["I4"].Value = "Tipo Doc";
                worksheet.Cells["J4"].Value = "Nro Doc";
                worksheet.Cells["K4"].Value = "Ape. Paterno";
                worksheet.Cells["L4"].Value = "Ape. Materno";
                worksheet.Cells["M4"].Value = "Nombres";
                worksheet.Cells["N4"].Value = "Celular";
                worksheet.Cells["O4"].Value = "Tel. Fijo";
                worksheet.Cells["P4"].Value = "Correo";
                worksheet.Cells["Q4"].Value = "Designacion Resol.";
                worksheet.Cells["A4:Q4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:Q4"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:Q4"].Style.Font.Bold = true;

                row = 5;

                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.iIdComite;
                    worksheet.Cells[row, 2].Value = item.iIdComiteNavigation.vUbigeo;
                    worksheet.Cells[row, 3].Value = item.dpto;
                    worksheet.Cells[row, 4].Value = item.provincia;
                    worksheet.Cells[row, 5].Value = item.distrito;
                    worksheet.Cells[row, 6].Value = item.iIdComiteNavigation.iTipResolucionNavigation.vDescripcion;
                    worksheet.Cells[row, 7].Value = item.iIdComiteNavigation.vNumResolucion;
                    worksheet.Cells[row, 8].Value = item.iTipCargoNavigation.vDescripcion;
                    worksheet.Cells[row, 9].Value = item.iCodPersonaNavigation.iTipDocumentoNavigation.vDescripcion;
                    worksheet.Cells[row, 10].Value = item.iCodPersonaNavigation.vNroDocumento;
                    worksheet.Cells[row, 11].Value = item.iCodPersonaNavigation.vApePaterno;
                    worksheet.Cells[row, 12].Value = item.iCodPersonaNavigation.vApeMaterno;
                    worksheet.Cells[row, 13].Value = item.iCodPersonaNavigation.vNombre;
                    worksheet.Cells[row, 14].Value = item.iCodPersonaNavigation.vCelular;
                    worksheet.Cells[row, 15].Value = item.iCodPersonaNavigation.vTelFijo;
                    worksheet.Cells[row, 16].Value = item.iCodPersonaNavigation.vEmail;
                    worksheet.Cells[row, 17].Value = item.bDesigResolucion.HasValue ? "SI" : "NO";
                    row++;
                }

                var sRango = "A4:Q" + (row - 1).ToString();
                worksheet.Cells[sRango].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells[sRango].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[sRango].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[sRango].AutoFitColumns();
                worksheet.Cells[sRango].Style.HorizontalAlignment = ExcelHorizontalAlignment.General;
                worksheet.Cells[sRango].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                //worksheet.Cells[sRango].Style.WrapText = true;

                xlPackage.Workbook.Properties.Title = "Data Relevante";
                xlPackage.Workbook.Properties.Author = "Israel Lozano del Castillo danielitolozano85@gmail.com";
                xlPackage.Workbook.Properties.Subject = "Data Relevante Empresarial";
                xlPackage.Save();
                // Response.Clear();
            }
            stream.Position = 0;
            return stream;
        }

        #endregion
    }
}
