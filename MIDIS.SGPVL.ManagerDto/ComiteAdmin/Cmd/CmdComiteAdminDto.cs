using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MIDIS.SGPVL.ManagerDto.ComiteAdmin.Cmd
{
    public class CmdComiteAdminDto
    {
        public int iIdComite { get; set; }
        [Required(ErrorMessage = "Campo Tipo Resolucion obligatorio")]
        [Display(Name = "Tipo Resolucion")]
        public int iTipResolucion { get; set; }
        [Required(ErrorMessage = "Campo Nro Resolucion obligatorio")]
        [Display(Name = "Nro Resolucion")]
        public string vNumResolucion { get; set; }
        [Required(ErrorMessage = "Campo Fecha Emision obligatorio")]
        [Display(Name = "Fecha Emision")]
        public DateTime dFecEmision { get; set; }
        [Required(ErrorMessage = "Campo Fecha Inicio obligatorio")]
        [Display(Name = "Fecha Inicio")]
        public DateTime dFecInicio { get; set; }
        [Required(ErrorMessage = "Campo Fecha Fin obligatorio")]
        [Display(Name = "Fecha Fin")]
        public DateTime dFecFin { get; set; }


        [Display(Name = "Observacion")]
        public string? vObservacion { get; set; }

        //Adicionales
        public string? ubigeoFull { get; set; }
        public string? tipoResolucionText { get; set; }

        [Required(ErrorMessage = "Archivo obligatorio")]
        public IFormFile FileResol { get; set; }

        public string? nombreDptoSelect { get; set; }
        public string? nombreProvSelect { get; set; }
        public string? nombreDistSelect { get; set; }
        public string? vUbigeo { get; set; }
        public string? vNomArchivo { get; set; }
        public string? vNomArcGuid { get; set; }

    }
}
