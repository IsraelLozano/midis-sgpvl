using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MIDIS.SGPVL.ManagerDto.ComitePvl.Cmd
{
    public class CmdComiteJdDto
    {
        public int iIdJunta { get; set; }
        public int iCodComVasLeche { get; set; }
        [Required(ErrorMessage = "Campo Tipo Resolucion obligatorio")]
        [Display(Name = "Tipo Resolucion")]
        public int iTipResolucion { get; set; }
        [Required(ErrorMessage = "Campo Nro Resolucion obligatorio")]
        [Display(Name = "Nro Resolucion")]
        public string vNumResolucion { get; set; }
        [Required(ErrorMessage = "Campo Fecha Emision obligatorio")]
        [Display(Name = "Fecha Emision")]
        public DateTime? dFecEmision { get; set; }
        [Required(ErrorMessage = "Campo Fecha Inicio obligatorio")]
        [Display(Name = "Fecha Inicio")]
        public DateTime? dFecInicio { get; set; }
        [Required(ErrorMessage = "Campo Fecha Fin obligatorio")]
        [Display(Name = "Fecha Fin")]
        public DateTime? dFecFin { get; set; }
        public string? vNomArchivo { get; set; }
        public string? vNomArcGuid { get; set; }
        [Display(Name = "Observacion")]
        public string vObservacion { get; set; }
        [Required(ErrorMessage = "Archivo obligatorio")]
        public IFormFile FileResol { get; set; }
    }
}
