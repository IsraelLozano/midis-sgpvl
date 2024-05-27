using MIDIS.SGPVL.ManagerDto.ComitePvl.Cmd;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Get;

namespace MIDIS.SGPVL.Manager.ComitePvl
{
    public interface ISocioManager
    {
        Task<CmdSocioDto> AddSocioAsync(CmdSocioDto model);
        Task<bool> DeleteSocioAsync(int id);
        Task<CmdSocioDto> GetSocioByIdAsync(int id);
        Task<List<GetSocioDto>> GetListSocioByComiteAsync(int idComite);
        Task<List<GetSocioDto>> GetListSocioByUbigeo(string idUbigeo);
    }
}