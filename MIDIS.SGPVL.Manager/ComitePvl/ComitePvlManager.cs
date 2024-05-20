using AutoMapper;
using MIDIS.SGPVL.Entity.Models.ComitePvl;
using MIDIS.SGPVL.Manager.Maestro;
using MIDIS.SGPVL.Manager.Settings;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Cmd;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Get;
using MIDIS.SGPVL.ManagerDto.Maestro.Get;
using MIDIS.SGPVL.Repository.UnitOfWork;

namespace MIDIS.SGPVL.Manager.ComitePvl
{
    public class ComitePvlManager : IComitePvlManager
    {
        private readonly IMapper _mapper;
        private readonly MaestroUnitOfWork _maestraUnitOfWork;
        private readonly IMaestroManager _maestraManager;
        private readonly IAplicationConstants _aplicationConstants;
        private readonly ComiteUnitOfWork _comiteUnitOfWork;

        public ComitePvlManager(IMapper mapper,
            MaestroUnitOfWork maestraUnitOfWork,
            IMaestroManager maestraManager,
            IAplicationConstants aplicationConstants,
            ComiteUnitOfWork comiteUnitOfWork)
        {
            _mapper = mapper;
            _maestraUnitOfWork = maestraUnitOfWork;
            _maestraManager = maestraManager;
            _aplicationConstants = aplicationConstants;
            _comiteUnitOfWork = comiteUnitOfWork;
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
            var ubigeo = string.IsNullOrEmpty(param.idCentroPoblado) ? $"{param.idDptoSelect}{param.idProvSelect}{param.idUbigeo}" : param.idCentroPoblado;
            var filtro = param.search?.value ?? "";

            var query = new List<VLComVasoLeche>();

            if (!string.IsNullOrEmpty(param.idCentroPoblado))
            {
                query = _comiteUnitOfWork
                ._comitePVLRepository
                .GetAll(l =>
                l.vCodCentPoblado == param.idCentroPoblado
                && (filtro.Contains(l.vNomComite + l.vCodComite) || filtro == ""),
                includeProperties: "iTipAlimentoNavigation,iTipOsbNavigation,VLJunDirectivas.iTipResolucionNavigation,VLJunDirectivas.VLMiembroJunta.iCodPersonaNavigation,VLJunDirectivas.VLMiembroJunta.iTipCargoNavigation");
            }
            else
            {
                query = _comiteUnitOfWork
                ._comitePVLRepository
                .GetAll(l =>
                l.vCodCentPoblado.StartsWith(ubigeo)
                && (filtro.Contains(l.vNomComite + l.vCodComite) || filtro == ""),
                includeProperties: "iTipAlimentoNavigation,iTipOsbNavigation,VLJunDirectivas.iTipResolucionNavigation,VLJunDirectivas.VLMiembroJunta.iCodPersonaNavigation,VLJunDirectivas.VLMiembroJunta.iTipCargoNavigation");
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



    }
}
