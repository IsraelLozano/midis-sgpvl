﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using MIDIS.SGPVL.Entity.Models.Maestro;
using MIDIS.SGPVL.Entity.Models.Persona;

namespace MIDIS.SGPVL.Entity.Models.ComitePvl
{
    public partial class VLUsuario
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
        public string vUsuRegistro { get; set; }
        public DateTime? dFecRegistro { get; set; }
        public string vUsuModifica { get; set; }
        public DateTime? dFecModifica { get; set; }

        public VLEnumItem iClasificacionNavigation { get; set; }
        public VLComVasoLeche iCodComVasLecheNavigation { get; set; }
        public VLPerNatural iCodPersonaNavigation { get; set; }
        public VLSocio iIdSocioNavigation { get; set; }
    }
}