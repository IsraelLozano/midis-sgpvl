using MIDIS.SGPVL.ManagerDto.Maestro.Get;
using MIDIS.SGPVL.ManagerDto.Persona;

namespace MIDIS.SGPVL.ManagerDto.ComitePvl.Get
{
    public class GetSocioDto
    {
        public int iIdSocio { get; set; }
        public int iCodPersona { get; set; }
        public int iCodComVasLeche { get; set; }
        public int iTipSocio { get; set; }
        public bool? bGestante { get; set; }
        public bool? bDiscapacitado { get; set; }
        public int? iNumSemGestacion { get; set; }
        public DateTime? dFecParto { get; set; }
        public DateTime? dFecTermLactancia { get; set; }
        public bool? bActivo { get; set; }

        public GetPersonaNaturalDto iCodPersonaNavigation { get; set; }
        public GetEnumeradoComboDto iTipSocioNavigation { get; set; }
        public string nombreCompleto { get; set; }
        public string nombreFull() => $"{iCodPersonaNavigation.vApePaterno} {iCodPersonaNavigation.vApeMaterno}, {iCodPersonaNavigation.vNombre}";
    }
}
