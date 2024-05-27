using MIDIS.SGPVL.ManagerDto.Maestro.Get;
using MIDIS.SGPVL.ManagerDto.Persona;

namespace MIDIS.SGPVL.ManagerDto.ComitePvl.Get
{
    public class GetBeneficiarioDto
    {
        public int iIdUsuario { get; set; }
        public int iCodPersona { get; set; }
        public int iCodComVasLeche { get; set; }
        public int? iIdSocio { get; set; }
        public int? iClasificacion { get; set; }
        public string vClaSocEconomica { get; set; }
        public bool? bGestante { get; set; }
        public bool? bDiscapacitado { get; set; }
        public bool? bPacTBC { get; set; }
        public int? iNumSemGestacion { get; set; }
        public DateTime? dFecParto { get; set; }
        public DateTime? dFecTermLactancia { get; set; }
        public bool? bActivo { get; set; }
        public string vUsuRegistro { get; set; }
        public DateTime? dFecRegistro { get; set; }
        public string vUsuModifica { get; set; }
        public DateTime? dFecModifica { get; set; }

        public GetEnumeradoComboDto iClasificacionNavigation { get; set; }
        public GetPersonaNaturalDto iCodPersonaNavigation { get; set; }
        public GetSocioDto iIdSocioNavigation { get; set; }
        public string fullNameSocio { get; set; }
        public string NroDocumentoSocio { get; set; }

        public string fullNameUsuario { get; set; }
        public string NroDocUsuario { get; set; }
        public string fechaNacimiento { get; set; }
        public int edad { get; set; }

    }
}
