using MIDIS.SGPVL.ManagerDto.Seguridad;

namespace MIDIS.SGPVL.Manager.Settings
{
    public interface IAplicationConstants
    {
        UsuarioSesionBE UsuarioSesionBE { get; set; }
        List<OpcionSesionBE> OpcionSesionBE { get; set; }
        string ImagenSesionUsuario { get; set; }
        string IpClienteCliente { get; }
        DateTime FechaHoy { get; }
        string RutaServidor { get; }
    }
}
