﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using MIDIS.SGPVL.Entity.Models.Maestro;
using MIDIS.SGPVL.Entity.Models.Persona;

namespace MIDIS.SGPVL.Entity.Models.Comite
{
    public partial class VLAdmMiembro
    {
        public int iIdMiembro { get; set; }
        public int iCodPersona { get; set; }
        public int iIdComite { get; set; }
        public int iTipCargo { get; set; }
        public bool? bDesigResolucion { get; set; }
        public string vOtrCargo { get; set; }
        public bool? bActivo { get; set; }
        public string vUsuRegistro { get; set; }
        public DateTime? dFecRegistro { get; set; }
        public string vUsuModifica { get; set; }
        public DateTime? dFecModifica { get; set; }

        public VLPerNatural iCodPersonaNavigation { get; set; }
        public VLAdministrativo iIdComiteNavigation { get; set; }
        public VLEnumItem iTipCargoNavigation { get; set; }
    }
}