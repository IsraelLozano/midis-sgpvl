using MIDIS.SGPVL.ManagerDto.Maestro.Get;

namespace MIDIS.SGPVL.ManagerDto.Persona
{
    public class GetPersonaNaturalDto
    {
        public int iCodPersona { get; set; }
        public string vApePaterno { get; set; }
        public string vApeMaterno { get; set; }
        public string vNombre { get; set; }
        public string vNroDocumento { get; set; }
        public bool? bActivo { get; set; }
        public int iTipDocumento { get; set; }
        public bool? bSexo { get; set; }
        public string vTelFijo { get; set; }
        public string vCelular { get; set; }
        public string vEmail { get; set; }
        public string vDireccion { get; set; }
        public string vUbigeo { get; set; }
        public DateTime? dFecNacimiento { get; set; }

        public GetEnumeradoComboDto iTipDocumentoNavigation { get; set; }
    }
}
