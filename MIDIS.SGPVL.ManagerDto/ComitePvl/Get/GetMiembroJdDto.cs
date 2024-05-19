using MIDIS.SGPVL.ManagerDto.Maestro.Get;
using MIDIS.SGPVL.ManagerDto.Persona;

namespace MIDIS.SGPVL.ManagerDto.ComitePvl.Get
{
    public class GetMiembroJdDto
    {
        public int iIdMiembro { get; set; }
        public int iCodPersona { get; set; }
        public int iIdJunta { get; set; }
        public int iTipCargo { get; set; }
        public bool? bActivo { get; set; }

        public GetPersonaNaturalDto iCodPersonaNavigation { get; set; }
        public GetEnumeradoComboDto iTipCargoNavigation { get; set; }
    }
}
