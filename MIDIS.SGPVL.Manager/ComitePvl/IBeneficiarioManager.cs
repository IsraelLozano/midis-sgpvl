using MIDIS.SGPVL.ManagerDto.ComitePvl.Cmd;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Get;

namespace MIDIS.SGPVL.Manager.ComitePvl
{
    public interface IBeneficiarioManager
    {
        Task<CmdBeneficiarioDto> AddBeneficiarioAsync(CmdBeneficiarioDto model);
        Task<bool> DeleteBeneficiarioAsync(int id);
        Task<CmdBeneficiarioDto> GetBeneficiarioByIdAsync(int id);
        Task<List<GetBeneficiarioDto>> GetListBeneficiarioByComiteAsync(int idComite);
    }
}