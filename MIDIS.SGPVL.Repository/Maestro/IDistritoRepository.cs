using MIDIS.SGPVL.Entity.Models.Maestro;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.Maestro
{
    public interface IDistritoRepository : IGenericRepository<MaeDistrito>
    {
        Task<List<MaeDistrito>> GetDistritoFullByParamAsync(List<string> ubigeos);
    }
}