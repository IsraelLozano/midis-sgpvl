using MIDIS.SGPVL.ManagerDto.ComitePvl.Cmd;

namespace MIDIS.SGPVL.Manager.ComitePvl
{
    public interface IBeneficiarioManager
    {
        Task<CmdBeneficiarioDto> AddBeneficiarioAsync(CmdBeneficiarioDto model);
        Task<bool> DeleteBeneficiarioAsync(int id);
        Task<CmdBeneficiarioDto> GetBeneficiarioByIdAsync(int id);
        Task<List<CmdBeneficiarioDto>> GetListBeneficiarioByComiteAsync(int idComite);
    }
}