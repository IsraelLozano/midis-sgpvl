using MIDIS.SGPVL.Entity.Models.Maestro;
using MIDIS.SGPVL.Utils.Enumerados;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.Maestro
{
    public interface IEnumeradoItemRepository : IGenericRepository<VLEnumItem>
    {
        Task<Dictionary<EnumeradoCabecera, List<VLEnumItem>>> GetListEnumeradoByGrupo(List<EnumeradoCabecera> listaMaestra);
    }
}