﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MIDIS.SGPVL.Entity.Models.Maestro
{
    public partial class MaeDepartamento
    {
        public string vCodigoDepartamento { get; set; }
        public string vDescripcion { get; set; }
        public bool bEstado { get; set; }

        public List<MaeProvincium> MaeProvincia { get; set; }
    }
}