using System.ComponentModel.DataAnnotations;

namespace MIDIS.SGPVL.ManagerDto.ComitePvl.Cmd
{
    public class CmdBeneficiarioDto
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
        public bool? iNumSemGestacion { get; set; }
        public DateTime? dFecParto { get; set; }
        public DateTime? dFecTermLactancia { get; set; }
        public bool? bActivo { get; set; }

        #region Datos-Persona
        [Display(Name = "Ape. Paterno")]
        [Required(ErrorMessage = "El Campo Ape. Paterno es obligatorio")]
        public string vApePaterno { get; set; }
        [Display(Name = "Ape. Materno")]
        [Required(ErrorMessage = "El Campo Ape. Materno es obligatorio")]
        public string vApeMaterno { get; set; }
        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El Campo Nombres es obligatorio")]
        public string vNombre { get; set; }
        [Display(Name = "Nro Documento")]
        [Required(ErrorMessage = "El Campo Nro Documento es obligatorio")]
        public string vNroDocumento { get; set; }
        [Display(Name = "Tipo Doc")]
        [Required(ErrorMessage = "El Campo Tipo Documento es obligatorio")]
        public int iTipDocumento { get; set; }
        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "El Campo Sexo es obligatorio")]
        public int bSexo { get; set; }
        [Display(Name = "Tel. Fijo")]
        public string? vTelFijo { get; set; }
        [Display(Name = "Nro Celular")]
        [Required(ErrorMessage = "El Campo Nro Celular es obligatorio")]
        public string vCelular { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "El Campo Email es obligatorio")]
        public string vEmail { get; set; }
        [Display(Name = "Direccion")]
        [Required(ErrorMessage = "El Campo Direccion es obligatorio")]
        public string vDireccion { get; set; }
        #endregion
    }
}
