using MIDIS.SGPVL.AppWeb.Models;
using MIDIS.SGPVL.ManagerDto.ComiteAdmin.Cmd;
using MIDIS.SGPVL.ManagerDto.ComiteAdmin.Get;
using MIDIS.SGPVL.ManagerDto.Maestro.Get;

namespace MIDIS.SGPVL.Manager.ComiteAdmin
{
    public interface IComiteAdminManager
    {
        Task<CmdComiteAdminDto> AddComiteAdmin(CmdComiteAdminDto model);
        Task<CmdComiteMemberAdminDto> AddComiteMemberAdmin(CmdComiteMemberAdminDto model);
        Task<List<GetAdministrativoDto>> GetAdministrativo(GetAdminParams param);
        Task<CmdComiteAdminDto> GetAdministrativoByIdAsync(int id);
        Task<GetAdministrativoFiltersDto> GetAdministrativoFilters(string filter);
        Task<MemoryStream> GetExcelComiteAdministrativoAsync(string codUbigeo);
        Task<MemoryStream> GetExcelComiteMembersAdminiAsync(string codUbigeo);
        Task<List<GetAdminMiembroDto>> GetMiembroByIdComiteAsync(int idAdmin);
    }
}