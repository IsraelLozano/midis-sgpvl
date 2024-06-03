using WsMigracion;
using WsReniec;

namespace MIDIS.SGPVL.Manager.AgenteExterno
{
    public interface IAgentExternalManager
    {
        Task<MigracionesCE_Registro> GetPersonaMigraciones(string nroDocumento);
        Task<ReniecPersona_Registro> GetPersonaReniec(string nroDocumento);
    }
}