﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using MIDIS.SGPVL.Entity.Models.Comite;
using MIDIS.SGPVL.Entity.Models.ComitePvl;
using MIDIS.SGPVL.Entity.Models.Maestro;
using MIDIS.SGPVL.Entity.Models.Persona;
#nullable disable

namespace MIDIS.SGPVL.Contexto.Data
{
    public partial class BDPVLContext : DbContext
    {
        public BDPVLContext()
        {
        }

        public BDPVLContext(DbContextOptions<BDPVLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<VLAdmMiembro> VLAdmMiembros { get; set; }
        public virtual DbSet<VLAdministrativo> VLAdministrativos { get; set; }
        public virtual DbSet<VLComVasoLeche> VLComVasoLeches { get; set; }
        public virtual DbSet<VLEnumItem> VLEnumItems { get; set; }
        public virtual DbSet<VLEnumerado> VLEnumerados { get; set; }
        public virtual DbSet<VLJunDirectiva> VLJunDirectivas { get; set; }
        public virtual DbSet<VLMiembroJuntum> VLMiembroJunta { get; set; }
        public virtual DbSet<VLPerNatural> VLPerNaturals { get; set; }
        public virtual DbSet<VLPersona> VLPersonas { get; set; }
        public virtual DbSet<VLSocio> VLSocios { get; set; }
        public virtual DbSet<VLUsuario> VLUsuarios { get; set; }
        public virtual DbSet<MaeCentroPoblado> MaeCentroPoblados { get; set; }
        public virtual DbSet<MaeDepartamento> MaeDepartamentos { get; set; }
        public virtual DbSet<MaeDistrito> MaeDistritos { get; set; }
        public virtual DbSet<MaeProvincium> MaeProvincia { get; set; }
        public virtual DbSet<ubigeo> ubigeos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.VLAdmMiembroConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.VLAdministrativoConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.VLComVasoLecheConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.VLEnumItemConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.VLEnumeradoConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.VLJunDirectivaConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.VLMiembroJuntumConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.VLPerNaturalConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.VLPersonaConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.VLSocioConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.VLUsuarioConfiguration());

            modelBuilder.ApplyConfiguration(new Configurations.MaeCentroPobladoConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.MaeDepartamentoConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.MaeDistritoConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.MaeProvinciumConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ubigeoConfiguration());


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
