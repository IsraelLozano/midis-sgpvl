﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIDIS.SGPVL.Entity.Models.Persona;
namespace MIDIS.SGPVL.Contexto.Data.Configurations
{
    public partial class VLPersonaConfiguration : IEntityTypeConfiguration<VLPersona>
    {
        public void Configure(EntityTypeBuilder<VLPersona> entity)
        {
            entity.HasKey(e => e.iCodPersona)
                .HasName("PK__VLPerson__C021D7532B6F21E2");

            entity.ToTable("VLPersona", "Persona");

            entity.Property(e => e.vNomComercial)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.vNomCompleto)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.vRuc)
                .HasMaxLength(11)
                .IsUnicode(false);

            entity.HasOne(d => d.iTipPersonaNavigation)
                .WithMany(p => p.VLPersonas)
                .HasForeignKey(d => d.iTipPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VLPersona__iTipP__4589517F");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<VLPersona> entity);
    }
}