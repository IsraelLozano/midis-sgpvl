using MIDIS.SGPVL.ManagerDto.Maestro.Get;
using MIDIS.SGPVL.Utils.Enumerados;

namespace MIDIS.SGPVL.Manager.Maestro
{
    public interface IMaestroManager
    {
        Task<List<GetDistritoDto>> getDistritoByDptoProv(string codDpto, string codProv);
        Task<List<GetProvinciaDto>> getProvinciaByDpto(string codDpto);
        Task<List<GetEnumeradoDto>> GetListEnumerado();
        Task<Dictionary<EnumeradoCabecera, List<GetEnumeradoComboDto>>> GetListEnumeradoByGrupo(List<EnumeradoCabecera> listaMaestra);
        Task<List<GetUbigeoDto>> getUbigeosByProvincia(string codProv);
        Task<List<GetDistritoDto>> getDistritoFull(List<string> ubigeo);
        Task<List<GetCentroPobladoDto>> getCentroPobladoByDistrito(string codDistrito);
        Task<List<GetCentroPobladoDto>> getCentroPobladoFull(List<string> codCentPoblados);
        Task<List<GetDptoDto>> GetAllDptoAsync();
    }
}