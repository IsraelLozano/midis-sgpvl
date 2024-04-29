using MIDIS.SGPVL.ManagerDto.Maestro.Get;
using MIDIS.SGPVL.Utils.Enumerados;

namespace MIDIS.SGPVL.Manager.Maestro
{
    public interface IMaestroManager
    {
        Task<List<GetEnumeradoDto>> GetListEnumerado();
        Task<Dictionary<EnumeradoCabecera, List<GetEnumeradoComboDto>>> GetListEnumeradoByGrupo(List<EnumeradoCabecera> listaMaestra);
    }
}