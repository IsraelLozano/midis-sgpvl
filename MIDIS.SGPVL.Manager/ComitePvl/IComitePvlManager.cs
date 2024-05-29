using MIDIS.SGPVL.ManagerDto.ComiteAdmin.Cmd;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Cmd;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Get;

namespace MIDIS.SGPVL.Manager.ComitePvl
{
    public interface IComitePvlManager
    {
        Task<CmdComiteDto> AddComitePvl(CmdComiteDto model);
        Task<CmdComiteDto> GetComiteByIdAsync(int id);
        Task<GetComiteParams> GetComiteFilters(string filter);
        Task<List<GetComiteDto>> GetComiteList(GetComiteParams param);
        Task<MemoryStream> GetExcelComitePvlAsync(string codUbigeo);
        Task<MemoryStream> GetExcelJuntaDirectivaAsync(string codUbigeo);
        Task<MemoryStream> GetExcelSocioAsync(string codUbigeo);
        Task<MemoryStream> GetExcelUsuariosAsync(string codUbigeo);
    }
}