using AutoMapper;
using MIDIS.SGPVL.Entity.Models.Maestro;
using MIDIS.SGPVL.ManagerDto.Maestro.Get;
using MIDIS.SGPVL.Repository.UnitOfWork;
using MIDIS.SGPVL.Utils.Enumerados;

namespace MIDIS.SGPVL.Manager.Maestro
{
    public class MaestroManager : IMaestroManager
    {
        private readonly MaestroUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaestroManager(MaestroUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Ubigeos

        public async Task<List<GetUbigeoDto>> getUbigeosByProvincia(string codProv)
        {
            try
            {
                var querys = _unitOfWork._ubigeoRepository.GetAll(l => l.cod_prov_inei.Equals(codProv), orderBy: l => l.OrderBy(s => s.desc_prov_inei));
                var response = _mapper.Map<List<GetUbigeoDto>>(querys);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetProvinciaDto>> getProvinciaByDpto(string codDpto)
        {
            try
            {
                var querys = _unitOfWork._provinciaRepository
                    .GetAll(l => l.vCodigoDepartamento.Equals(codDpto) && !l.vCodigoProvincia.Equals("00"), orderBy: l => l.OrderBy(s => s.vDescripcion));
                var response = _mapper.Map<List<GetProvinciaDto>>(querys);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetDistritoDto>> getDistritoByDptoProv(string codDpto, string codProv)
        {
            try
            {
                var querys = _unitOfWork._distritoRepository
                    .GetAll(l =>
                    l.vCodigoDepartamento.Equals(codDpto) &&
                    l.vCodigoProvincia.Equals(codProv) &&
                    !l.vCodigoDistrito.Equals("00"), orderBy: l => l.OrderBy(s => s.vDescripcion));

                var response = _mapper.Map<List<GetDistritoDto>>(querys);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetCentroPobladoDto>> getCentroPobladoByDistrito(string codDistrito)
        {
            try
            {
                var querys = _unitOfWork._centroPobladoRepository
                    .GetAll(l =>
                    l.vUbigeo.StartsWith(codDistrito), orderBy: l => l.OrderBy(s => s.vCentroPoblado));

                var response = _mapper.Map<List<GetCentroPobladoDto>>(querys);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetDistritoDto>> getDistritoFull(List<string> ubigeo)
        {
            try
            {
                var querys = await _unitOfWork._distritoRepository.GetDistritoFullByParamAsync(ubigeo);
                var response = _mapper.Map<List<GetDistritoDto>>(querys);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        
        public async Task<List<GetCentroPobladoDto>> getCentroPobladoFull(List<string> codCentPoblados)
        {
            try
            {
                var querys = _unitOfWork._centroPobladoRepository.GetAll(l => codCentPoblados.Contains(l.vUbigeo));
                var response = _mapper.Map<List<GetCentroPobladoDto>>(querys);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Enumerados
        public async Task<List<GetEnumeradoDto>> GetListEnumerado()
        {
            try
            {
                var query = _mapper.Map<List<GetEnumeradoDto>>(_unitOfWork._enumeradoRepository.GetAll(l => l.bActivo.Value, orderBy: l => l.OrderBy(o => o.vDescripcion)));
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Dictionary<EnumeradoCabecera, List<GetEnumeradoComboDto>>> GetListEnumeradoByGrupo(List<EnumeradoCabecera> listaMaestra)
        {
            try
            {
                Dictionary<EnumeradoCabecera, List<VLEnumItem>> lista = await _unitOfWork._enumeradoItemRepository.GetListEnumeradoByGrupo(listaMaestra);
                var listaEntera = listaMaestra.Select(x => (int)x).ToList();
                Dictionary<EnumeradoCabecera, List<GetEnumeradoComboDto>> listaRes = new Dictionary<EnumeradoCabecera, List<GetEnumeradoComboDto>>();
                foreach (EnumeradoCabecera item in listaEntera)
                {
                    listaRes.Add(item, _mapper.Map<List<GetEnumeradoComboDto>>(lista[item]));
                }
                return listaRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
