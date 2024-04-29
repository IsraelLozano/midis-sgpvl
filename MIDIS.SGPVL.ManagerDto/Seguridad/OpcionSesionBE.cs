namespace MIDIS.SGPVL.ManagerDto.Seguridad
{
    public class OpcionSesionBE
    {
        public OpcionSesionBE()
        {
            
        }

        public OpcionSesionBE(OpcionDto opcion)
        {
            IdOpcion = opcion.Id_Opcion;
            NombreOpcion = opcion.Nombre;
            NombreIcono = opcion.Icono;
            IdOpcionRef = opcion.Id_Opcion_Ref;
            RutaGenerica = opcion.Url_Opcion;
            Posicion = opcion.Orden;
            string[] reg = opcion.Url_Opcion.Split("/");

            switch (reg.Length)
            {
                case 1:
                    Controladora = opcion.Url_Opcion;
                    Accion = "Index";
                    break;
                case 2:
                    Controladora = reg[0];
                    Accion = reg[1];
                    break;
                case 3:
                    Area = reg[0];
                    Controladora = reg[1];
                    Accion = reg[2];
                    break;
                default:
                    break;
            }
        }
        public int IdOpcion { get; set; }
        public string NombreOpcion { get; set; }
        public string NombreIcono { get; set; }
        public string Area { get; set; }
        public string Controladora { get; set; }
        public string Accion { get; set; }
        public string RutaGenerica { get; set; }
        public int IdOpcionRef { get; set; }
        public int Posicion { get; set; }
    }
}
