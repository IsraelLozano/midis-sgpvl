using WsReniec;

namespace MIDIS.SGPVL.Manager.AgenteExterno
{
    public interface IAgentExternalManager
    {
        Task<ReniecPersona_Registro> GetPersonaReniec(string nroDocumento);
    }
}