﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIDIS.SGPVL.Entity.Models.ComitePvl;
namespace MIDIS.SGPVL.Contexto.Data.Configurations
{
    public partial class VLUsuarioConfiguration : IEntityTypeConfiguration<VLUsuario>
    {
        public void Configure(EntityTypeBuilder<VLUsuario> entity)
        {
            entity.HasKey(e => e.iIdUsuario)
                .HasName("PK__VLUsuari__A271047F4F418EA5");

            entity.ToTable("VLUsuario", "ComitePvl");

            entity.Property(e => e.dFecModifica).HasColumnType("datetime");

            entity.Property(e => e.dFecParto).HasColumnType("date");

            entity.Property(e => e.dFecRegistro).HasColumnType("datetime");

            entity.Property(e => e.dFecTermLactancia).HasColumnType("date");

            entity.Property(e => e.vClaSocEconomica)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.vUsuModifica)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.vUsuRegistro)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.iClasificacionNavigation)
                .WithMany(p => p.VLUsuarios)
                .HasForeignKey(d => d.iClasificacion)
                .HasConstraintName("FK__VLUsuario__iClas__57A801BA");

            entity.HasOne(d => d.iCodComVasLecheNavigation)
                .WithMany(p => p.VLUsuarios)
                .HasForeignKey(d => d.iCodComVasLeche)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VLUsuario__iCodC__55BFB948");

            entity.HasOne(d => d.iCodPersonaNavigation)
                .WithMany(p => p.VLUsuarios)
                .HasForeignKey(d => d.iCodPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VLUsuario__iCodP__54CB950F");

            entity.HasOne(d => d.iIdSocioNavigation)
                .WithMany(p => p.VLUsuarios)
                .HasForeignKey(d => d.iIdSocio)
                .HasConstraintName("FK__VLUsuario__iIdSo__56B3DD81");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<VLUsuario> entity);
    }
}
