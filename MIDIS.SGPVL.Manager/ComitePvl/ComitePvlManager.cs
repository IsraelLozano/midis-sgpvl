using AutoMapper;
using MIDIS.SGPVL.Entity.Models.ComitePvl;
using MIDIS.SGPVL.Manager.Maestro;
using MIDIS.SGPVL.Manager.Settings;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Cmd;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Get;
using MIDIS.SGPVL.ManagerDto.Maestro.Get;
using MIDIS.SGPVL.Repository.UnitOfWork;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using Azure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MIDIS.SGPVL.Manager.ComitePvl
{
    public class ComitePvlManager : IComitePvlManager
    {
        private readonly IMapper _mapper;
        private readonly MaestroUnitOfWork _maestraUnitOfWork;
        private readonly IMaestroManager _maestraManager;
        private readonly IAplicationConstants _aplicationConstants;
        private readonly ComiteUnitOfWork _comiteUnitOfWork;
        private readonly IJuntaDirectivaManager _juntaDirectivaManager;

        public ComitePvlManager(IMapper mapper,
            MaestroUnitOfWork maestraUnitOfWork,
            IMaestroManager maestraManager,
            IAplicationConstants aplicationConstants,
            ComiteUnitOfWork comiteUnitOfWork,
            IJuntaDirectivaManager juntaDirectivaManager)
        {
            _mapper = mapper;
            _maestraUnitOfWork = maestraUnitOfWork;
            _maestraManager = maestraManager;
            _aplicationConstants = aplicationConstants;
            _comiteUnitOfWork = comiteUnitOfWork;
            _juntaDirectivaManager = juntaDirectivaManager;
        }

        public async Task<GetComiteParams> GetComiteFilters(string filter)
        {
            var response = new GetComiteParams();
            response.dptos = _mapper.Map<List<GetDptoDto>>(_maestraUnitOfWork
                ._departamentoRepository
                .GetAll(l => l.vCodigoDepartamento != "00", orderBy: l => l.OrderBy(s => s.vDescripcion)));
            return response;
        }

        public async Task<List<GetComiteDto>> GetComiteList(GetComiteParams param)
        {
            string ubigeo = string.Empty;
            if (string.IsNullOrEmpty(param.idUbigeo))
            {
                ubigeo = $"{param.idDptoSelect}{param.idProvSelect}";
            }
            else
            {
                ubigeo = param.idUbigeo;
            }

            ubigeo = string.IsNullOrEmpty(param.idCentroPoblado) ? ubigeo : param.idCentroPoblado;

            var filtro = param.search?.value ?? "";

            var query = new List<VLComVasoLeche>();

            if (!string.IsNullOrEmpty(param.idCentroPoblado))
            {
                query = _comiteUnitOfWork
                ._comitePVLRepository
                .GetAll(l =>
                l.vCodCentPoblado == param.idCentroPoblado,
                //&& (filtro.Contains(l.vNomComite + l.vCodComite) || filtro == ""),
                includeProperties: "iTipAlimentoNavigation,iTipOsbNavigation,VLJunDirectivas.iTipResolucionNavigation,VLJunDirectivas.VLMiembroJunta.iCodPersonaNavigation.iTipDocumentoNavigation,VLJunDirectivas.VLMiembroJunta.iTipCargoNavigation");
            }
            else
            {
                query = _comiteUnitOfWork
                ._comitePVLRepository
                .GetAll(l =>
                l.vCodCentPoblado.StartsWith(ubigeo),
                //&& (filtro.Contains(l.vNomComite + l.vCodComite) || filtro == ""),
                includeProperties: "iTipAlimentoNavigation,iTipOsbNavigation,VLJunDirectivas.iTipResolucionNavigation,VLJunDirectivas.VLMiembroJunta.iCodPersonaNavigation.iTipDocumentoNavigation,VLJunDirectivas.VLMiembroJunta.iTipCargoNavigation");
            }


            var ubigeos = query.Select(s => s.vUbigeo).Distinct().ToList();
            var codCentPoblados = query.Select(s => s.vCodCentPoblado).Distinct().ToList();

            var listUbigeos = await _maestraManager.getDistritoFull(ubigeos);
            var listCentroPoblados = await _maestraManager.getCentroPobladoFull(codCentPoblados);

            var response = _mapper.Map<List<GetComiteDto>>(query);

            response.ForEach(u =>
            {
                var fullUbigeo = listUbigeos.FirstOrDefault(l => l.codigo == u.vUbigeo);
                u.ubigeoFull = fullUbigeo.full();
                u.centroPobladoFull = listCentroPoblados.FirstOrDefault(l => l.codigo == u.vCodCentPoblado).descripcion;
                var presidente = u.VLJunDirectivas.Where(l => l.VLMiembroJunta.Any(s => s.iTipCargo == 79)).Select(p => p.VLMiembroJunta.Select(o => o.iCodPersonaNavigation)).FirstOrDefault();
                var estActivo = u.VLJunDirectivas.Where(l => l.VLMiembroJunta.Any(s => s.iTipCargo == 79)).Select(p => p.VLMiembroJunta).FirstOrDefault();
                if (presidente != null)
                {
                    u.nombrePresidente = $"{presidente.First().vApePaterno} {presidente.First().vApeMaterno}, {presidente.First().vNombre}";
                    u.nroDocumento = $"{presidente.First().iTipDocumentoNavigation.descripcion}: {presidente.First().vNroDocumento}";
                    u.estado = estActivo.FirstOrDefault().bActivo.Value;
                }

            });
            return response;
        }

        public async Task<CmdComiteDto> GetComiteByIdAsync(int id)
        {
            var query = _comiteUnitOfWork._comitePVLRepository.GetAll(l => l.iCodComVasLeche == id);
            var ubigeos = query.Select(s => s.vUbigeo).Distinct().ToList();
            var listUbigeos = await _maestraManager.getDistritoFull(ubigeos);
            var response = _mapper.Map<CmdComiteDto>(query.FirstOrDefault());
            //response.ubigeoFull = listUbigeos.FirstOrDefault().full();
            //response.tipoResolucionText = listUbigeos.FirstOrDefault().full();
            return response;

        }

        public async Task<CmdComiteDto> AddComitePvl(CmdComiteDto model)
        {
            var entidad = _mapper.Map<VLComVasoLeche>(model);
            try
            {
                if (entidad.iCodComVasLeche == 0)
                {
                    entidad.dFecRegistro = entidad.dFecModifica = DateTime.Now;
                    entidad.vUsuRegistro = entidad.vUsuModifica = _aplicationConstants.UsuarioSesionBE.Credenciales;
                    entidad.bActivo = true;
                    entidad.vCodComite = entidad.vCodCentPoblado;

                    _comiteUnitOfWork._comitePVLRepository.Insert(entidad);
                }
                else
                {
                    //var query = _comiteUnitOfWork._comiteAdminRepository.GetById(model.iIdComite);
                    _comiteUnitOfWork._comitePVLRepository.Update(entidad);
                }
                var result = await _comiteUnitOfWork.SaveAsync() != 0;
                return _mapper.Map<CmdComiteDto>(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        #region Exportar - Excel

        public async Task<MemoryStream> GetExcelJuntaDirectivaAsync(string codUbigeo)
        {
            var data = await _juntaDirectivaManager
                    .GetMiembrosAllJuntaAsync();

            //var ubigeos = data.Select(s => s.iIdComiteNavigation.vUbigeo).Distinct().ToList();
            //var listUbigeos = await _maestraManager.getDistritoFull(ubigeos);
            //data.ForEach(u =>
            //{
            //    var fullUbigeo = listUbigeos.FirstOrDefault(l => l.codigo == u.iIdComiteNavigation.vUbigeo);
            //    u.dpto = fullUbigeo.dpto;
            //    u.provincia = fullUbigeo.provincia;
            //    u.distrito = fullUbigeo.descripcion;
            //});

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

                worksheet.Cells["A1"].Value = "LISTADO MIEMBROS DE JUNTA DIRECTIVA";
                using (var r = worksheet.Cells["A1:M1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Size = 15;
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["A4"].Value = "Codigo";
                worksheet.Cells["B4"].Value = "Tipo Resolucion";
                worksheet.Cells["C4"].Value = "Nro Resolucion";
                worksheet.Cells["D4"].Value = "Cargo";
                worksheet.Cells["F4"].Value = "Tipo Doc";
                worksheet.Cells["G4"].Value = "Nro Doc";
                worksheet.Cells["H4"].Value = "Ape. Paterno";
                worksheet.Cells["I4"].Value = "Ape. Materno";
                worksheet.Cells["J4"].Value = "Nombres";
                worksheet.Cells["K4"].Value = "Celular";
                worksheet.Cells["L4"].Value = "Tel. Fijo";
                worksheet.Cells["M4"].Value = "Correo";
                worksheet.Cells["A4:M4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:M4"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:M4"].Style.Font.Bold = true;

                row = 5;

                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.iIdJunta;
                    worksheet.Cells[row, 2].Value = 0;
                    worksheet.Cells[row, 3].Value = 0;
                    worksheet.Cells[row, 4].Value = item.iTipCargoNavigation.descripcion;
                    worksheet.Cells[row, 5].Value = item.iCodPersonaNavigation.iTipDocumentoNavigation.descripcion;
                    worksheet.Cells[row, 6].Value = item.iCodPersonaNavigation.vNroDocumento;
                    worksheet.Cells[row, 7].Value = item.iCodPersonaNavigation.vApePaterno;
                    worksheet.Cells[row, 8].Value = item.iCodPersonaNavigation.vApeMaterno;
                    worksheet.Cells[row, 9].Value = item.iCodPersonaNavigation.vNombre;
                    worksheet.Cells[row, 10].Value = item.iCodPersonaNavigation.vCelular;
                    worksheet.Cells[row, 11].Value = item.iCodPersonaNavigation.vTelFijo;
                    worksheet.Cells[row, 12].Value = item.iCodPersonaNavigation.vEmail;
                    row++;
                }

                var sRango = "A4:M" + (row - 1).ToString();
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


        public async Task<MemoryStream> GetExcelSocioAsync(string codUbigeo)
        {

            var data = _comiteUnitOfWork
                ._socioReposiroty
                .GetAll(includeProperties: "iCodComVasLecheNavigation,iTipSocioNavigation,iCodPersonaNavigation.iTipDocumentoNavigation");

            //var ubigeos = data.Select(s => s.iIdComiteNavigation.vUbigeo).Distinct().ToList();
            //var listUbigeos = await _maestraManager.getDistritoFull(ubigeos);
            //data.ForEach(u =>
            //{
            //    var fullUbigeo = listUbigeos.FirstOrDefault(l => l.codigo == u.iIdComiteNavigation.vUbigeo);
            //    u.dpto = fullUbigeo.dpto;
            //    u.provincia = fullUbigeo.provincia;
            //    u.distrito = fullUbigeo.descripcion;
            //});

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

                worksheet.Cells["A1"].Value = "LISTADO SOCIOS DE JUNTA DIRECTIVA";
                using (var r = worksheet.Cells["A1:O1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Size = 15;
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["A4"].Value = "Codigo";
                worksheet.Cells["B4"].Value = "Nombre Comite";
                worksheet.Cells["C4"].Value = "Tipo Socio";
                worksheet.Cells["D4"].Value = "Tipo Doc";
                worksheet.Cells["E4"].Value = "Nro Doc";
                worksheet.Cells["F4"].Value = "Ape. Paterno";
                worksheet.Cells["G4"].Value = "Ape. Materno";
                worksheet.Cells["H4"].Value = "Nombres";
                worksheet.Cells["I4"].Value = "Celular";
                worksheet.Cells["J4"].Value = "Tel. Fijo";
                worksheet.Cells["K4"].Value = "Gestante";
                worksheet.Cells["L4"].Value = "Discapacitado";
                worksheet.Cells["M4"].Value = "Nro Sem Gestacion";
                worksheet.Cells["N4"].Value = "Fecha Parto";
                worksheet.Cells["O4"].Value = "Fecha Termino Lactancia";
                worksheet.Cells["A4:O4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:O4"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:O4"].Style.Font.Bold = true;

                row = 5;

                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.iIdSocio;
                    worksheet.Cells[row, 2].Value = item.iCodComVasLecheNavigation.vNomComite;
                    worksheet.Cells[row, 3].Value = item.iTipSocioNavigation.vDescripcion;
                    worksheet.Cells[row, 4].Value = item.iCodPersonaNavigation.iTipDocumentoNavigation.vDescripcion;
                    worksheet.Cells[row, 5].Value = item.iCodPersonaNavigation.vNroDocumento;
                    worksheet.Cells[row, 6].Value = item.iCodPersonaNavigation.vApePaterno;
                    worksheet.Cells[row, 7].Value = item.iCodPersonaNavigation.vApeMaterno;
                    worksheet.Cells[row, 8].Value = item.iCodPersonaNavigation.vNombre;
                    worksheet.Cells[row, 9].Value = item.iCodPersonaNavigation.vCelular;
                    worksheet.Cells[row, 10].Value = item.iCodPersonaNavigation.vTelFijo;
                    worksheet.Cells[row, 11].Value = item.bGestante.Value ? "SI" : "NO";
                    worksheet.Cells[row, 12].Value = item.bDiscapacitado.Value ? "SI" : "NO";
                    worksheet.Cells[row, 13].Value = item.iNumSemGestacion.HasValue ? item.iNumSemGestacion.Value : 0;
                    worksheet.Cells[row, 14].Value = item.dFecParto.HasValue ? item.dFecParto.Value.ToShortDateString() : "";
                    worksheet.Cells[row, 15].Value = item.dFecTermLactancia.HasValue ? item.dFecTermLactancia.Value.ToShortDateString() : "";
                    row++;
                }

                var sRango = "A4:O" + (row - 1).ToString();
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


        public async Task<MemoryStream> GetExcelUsuariosAsync(string codUbigeo)
        {

            var data = _comiteUnitOfWork
                ._usuarioRepository
                .GetAll(
                includeProperties: "iCodComVasLecheNavigation,iIdSocioNavigation.iCodPersonaNavigation.iTipDocumentoNavigation," +
                "iClasificacionNavigation,iCodPersonaNavigation.iTipDocumentoNavigation");

            //var ubigeos = data.Select(s => s.iIdComiteNavigation.vUbigeo).Distinct().ToList();
            //var listUbigeos = await _maestraManager.getDistritoFull(ubigeos);
            //data.ForEach(u =>
            //{
            //    var fullUbigeo = listUbigeos.FirstOrDefault(l => l.codigo == u.iIdComiteNavigation.vUbigeo);
            //    u.dpto = fullUbigeo.dpto;
            //    u.provincia = fullUbigeo.provincia;
            //    u.distrito = fullUbigeo.descripcion;
            //});

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

                worksheet.Cells["A1"].Value = "LISTADO BENEFICIARIOS DE JUNTA DIRECTIVA";
                using (var r = worksheet.Cells["A1:O1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Size = 15;
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["A4"].Value = "Codigo";
                worksheet.Cells["B4"].Value = "Nombre Comite";
                worksheet.Cells["C4"].Value = "Tipo Prioridad";
                worksheet.Cells["D4"].Value = "Tipo Doc";
                worksheet.Cells["E4"].Value = "Nro Doc";
                worksheet.Cells["F4"].Value = "Ape. Paterno";
                worksheet.Cells["G4"].Value = "Ape. Materno";
                worksheet.Cells["H4"].Value = "Nombres";
                worksheet.Cells["I4"].Value = "Celular";
                worksheet.Cells["J4"].Value = "Tel. Fijo";
                worksheet.Cells["K4"].Value = "Gestante";
                worksheet.Cells["L4"].Value = "Discapacitado";
                worksheet.Cells["M4"].Value = "Nro Sem Gestacion";
                worksheet.Cells["N4"].Value = "Fecha Parto";
                worksheet.Cells["O4"].Value = "Fecha Termino Lactancia";
                worksheet.Cells["P4"].Value = "Pasciente TBC";
                worksheet.Cells["A4:O4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:O4"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:O4"].Style.Font.Bold = true;

                row = 5;

                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.iIdSocio;
                    worksheet.Cells[row, 2].Value = item.iCodComVasLecheNavigation.vNomComite;
                    worksheet.Cells[row, 3].Value = item.iClasificacionNavigation.vDescripcion;
                    worksheet.Cells[row, 4].Value = item.iCodPersonaNavigation.iTipDocumentoNavigation.vDescripcion;
                    worksheet.Cells[row, 5].Value = item.iCodPersonaNavigation.vNroDocumento;
                    worksheet.Cells[row, 6].Value = item.iCodPersonaNavigation.vApePaterno;
                    worksheet.Cells[row, 7].Value = item.iCodPersonaNavigation.vApeMaterno;
                    worksheet.Cells[row, 8].Value = item.iCodPersonaNavigation.vNombre;
                    worksheet.Cells[row, 9].Value = item.iCodPersonaNavigation.vCelular;
                    worksheet.Cells[row, 10].Value = item.iCodPersonaNavigation.vTelFijo;
                    worksheet.Cells[row, 11].Value = item.bGestante.Value ? "SI" : "NO";
                    worksheet.Cells[row, 12].Value = item.bDiscapacitado.Value ? "SI" : "NO";
                    worksheet.Cells[row, 13].Value = item.iNumSemGestacion.HasValue ? item.iNumSemGestacion.Value : 0;
                    worksheet.Cells[row, 14].Value = item.dFecParto.HasValue ? item.dFecParto.Value.ToShortDateString() : "";
                    worksheet.Cells[row, 15].Value = item.dFecTermLactancia.HasValue ? item.dFecTermLactancia.Value.ToShortDateString() : "";
                    worksheet.Cells[row, 16].Value = item.bPacTBC.Value ? "SI" : "NO";
                    row++;
                }

                var sRango = "A4:O" + (row - 1).ToString();
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
