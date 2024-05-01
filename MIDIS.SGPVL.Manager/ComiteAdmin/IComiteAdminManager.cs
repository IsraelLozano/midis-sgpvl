using MIDIS.SGPVL.ManagerDto.ComiteAdmin.Get;
using MIDIS.SGPVL.ManagerDto.Maestro.Get;

namespace MIDIS.SGPVL.Manager.ComiteAdmin
{
    public interface IComiteAdminManager
    {
        Task<List<GetAdministrativoDto>> GetAdministrativo(string filter);
        Task<GetAdministrativoFiltersDto> GetAdministrativoFilters(string filter);
    }
}