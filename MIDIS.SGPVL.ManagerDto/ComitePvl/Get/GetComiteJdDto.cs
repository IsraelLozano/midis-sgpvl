﻿using MIDIS.SGPVL.ManagerDto.Maestro.Get;

namespace MIDIS.SGPVL.ManagerDto.ComitePvl.Get
{
    public class GetComiteJdDto
    {
        public int iIdJunta { get; set; }
        public int iCodComVasLeche { get; set; }
        public int iTipResolucion { get; set; }
        public string vNumResolucion { get; set; }
        public DateTime dFecEmision { get; set; }
        public DateTime dFecInicio { get; set; }
        public DateTime? dFecFin { get; set; }
        public bool? bVigente { get; set; }
        public int iNumMiembro { get; set; }
        public string vNomArchivo { get; set; }
        public string vNomArcGuid { get; set; }
        public string vObservacion { get; set; }

        public GetEnumeradoComboDto iTipResolucionNavigation { get; set; }
        public List<GetMiembroJdDto> VLMiembroJunta { get; set; }
    }
}
