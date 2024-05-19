using MIDIS.SGPVL.ManagerDto.Maestro.Get;
using MIDIS.SGPVL.ManagerDto.Persona;

namespace MIDIS.SGPVL.ManagerDto.ComiteAdmin.Get
{
    public class GetAdminMiembroDto
    {
        public int iIdMiembro { get; set; }
        public int iCodPersona { get; set; }
        public int iIdComite { get; set; }
        public int iTipCargo { get; set; }
        public bool? bDesigResolucion { get; set; }
        public string vOtrCargo { get; set; }
        public GetPersonaNaturalDto iCodPersonaNavigation { get; set; }
        public GetEnumeradoComboDto iTipCargoNavigation { get; set; }
    }
}
