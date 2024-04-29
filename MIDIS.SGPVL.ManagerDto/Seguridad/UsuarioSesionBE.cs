namespace MIDIS.SGPVL.ManagerDto.Seguridad
{
    public class UsuarioSesionBE
    {
        public UsuarioSesionBE()
        {
            
        }

        public UsuarioSesionBE(int idUsuario, 
            int idPerfil, 
            int idEmpresa, 
            string credenciales, 
            string nombreCompleto, 
            string nombreImagen, 
            string nombreInstitucion)
        {
            IdUsuario = idUsuario;
            IdPerfil = idPerfil;
            IdEmpresa = idEmpresa;
            Credenciales = credenciales;
            NombreCompleto = nombreCompleto;
            NombreImagen = nombreImagen;
            NombreInstitucion = nombreInstitucion;
        }

        public int IdUsuario { get; set; }
        public int IdPerfil { get; set; }
        public int IdEmpresa { get; set; }
        public string Credenciales { get; set; }
        public string NombreCompleto { get; set; }
        public string NombreImagen { get; set; }
        public string NombreInstitucion { get; set; }

    }
}
