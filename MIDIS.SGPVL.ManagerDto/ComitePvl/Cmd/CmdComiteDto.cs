using System.ComponentModel.DataAnnotations;

namespace MIDIS.SGPVL.ManagerDto.ComitePvl.Cmd
{
    public class CmdComiteDto
    {
        public int iCodComVasLeche { get; set; }
        [Required(ErrorMessage = "Campo Tipo OSB obligatorio")]
        [Display(Name = "Tipo OSB")]
        public int iTipOsb { get; set; }
        [Required(ErrorMessage = "Campo Tipo Alimento obligatorio")]
        [Display(Name = "Tipo Alimento")]
        public int iTipAlimento { get; set; }
        public string? vCodComite { get; set; }
        public string vUbigeo { get; set; }
        public string vCodCentPoblado { get; set; }
        [Required(ErrorMessage = "Campo Nombre Comite obligatorio")]
        [Display(Name = "Nombre Comite")]
        public string vNomComite { get; set; }
        public bool? bActivo { get; set; }
        public int? iNumSocio { get; set; }
        public int? iNumUsuario { get; set; }
        [Required(ErrorMessage = "Campo Latitud obligatorio")]
        [Display(Name = "Latitud")]
        public string vLatitud { get; set; }
        [Required(ErrorMessage = "Campo Longitud obligatorio")]
        [Display(Name = "Longitud")]
        public string vLongitud { get; set; }
        [Required(ErrorMessage = "Campo Direccion obligatorio")]
        [Display(Name = "Direccion")]
        public string vDireccion { get; set; }
        [Required(ErrorMessage = "Campo Referencia obligatorio")]
        [Display(Name = "Referencia")]
        public string vReferencia { get; set; }
    }
}
