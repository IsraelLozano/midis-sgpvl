using MIDIS.SGPVL.ManagerDto.ComitePvl.Cmd;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Get;

namespace MIDIS.SGPVL.Manager.ComitePvl
{
    public interface IJuntaDirectivaManager
    {
        Task<CmdComiteJdDto> AddComiteAdmin(CmdComiteJdDto model);
        Task<CmdMiembroJdDto> AddComiteMemberAdmin(CmdMiembroJdDto model);
        Task<GetComiteJdDto> GetJuntaByIdAsync(int id);
        Task<List<GetComiteJdDto>> GetJuntaDirectivaByComiteAsync(int idComite);
        Task<List<GetMiembroJdDto>> GetMiembrosByJuntaAsync(int idJunta);
    }
}