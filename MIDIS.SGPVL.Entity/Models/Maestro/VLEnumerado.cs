﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
namespace MIDIS.SGPVL.Entity.Models.Maestro
{
    public partial class VLEnumerado
    {
        public int iIdEnumerado { get; set; }
        public string vDescripcion { get; set; }
        public string vDesCorto { get; set; }
        public string vDesLargo { get; set; }
        public string vValor { get; set; }
        public bool? bActivo { get; set; }

        public List<VLEnumItem> VLEnumItems { get; set; }
    }
}