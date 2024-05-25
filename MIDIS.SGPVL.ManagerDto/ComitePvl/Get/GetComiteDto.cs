using MIDIS.SGPVL.ManagerDto.Maestro.Get;

namespace MIDIS.SGPVL.ManagerDto.ComitePvl.Get
{
    public class GetComiteDto
    {
        public int iCodComVasLeche { get; set; }
        public int iTipOsb { get; set; }
        public int iTipAlimento { get; set; }
        public string vCodComite { get; set; }
        public string vUbigeo { get; set; }
        public string vCodCentPoblado { get; set; }
        public string vNomComite { get; set; }
        public bool? bActivo { get; set; }
        public int? iNumSocio { get; set; }
        public int? iNumUsuario { get; set; }
        public string vLatitud { get; set; }
        public string vLongitud { get; set; }
        public string vDireccion { get; set; }
        public string vReferencia { get; set; }

        public GetEnumeradoComboDto iTipAlimentoNavigation { get; set; }
        public GetEnumeradoComboDto iTipOsbNavigation { get; set; }
        public List<GetComiteJdDto> VLJunDirectivas { get; set; }
        public string ubigeoFull { get; set; }
        public string centroPobladoFull { get; set; }

        //Adcionales
        public string nombrePresidente { get; set; }
        public string nroDocumento { get; set; }
        public bool estado { get; set; }

    }
}
