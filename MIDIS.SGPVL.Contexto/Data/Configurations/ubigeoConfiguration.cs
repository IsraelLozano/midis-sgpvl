﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIDIS.SGPVL.Entity.Models.Maestro;

namespace MIDIS.SGPVL.Contexto.Data.Configurations
{
    public partial class ubigeoConfiguration : IEntityTypeConfiguration<ubigeo>
    {
        public void Configure(EntityTypeBuilder<ubigeo> entity)
        {
            entity.HasNoKey();

            entity.ToTable("ubigeo");

            entity.Property(e => e.cod_dep_inei)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();

            entity.Property(e => e.cod_dep_reniec)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();

            entity.Property(e => e.cod_dep_sunat)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();

            entity.Property(e => e.cod_prov_inei)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.Property(e => e.cod_prov_reniec)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.Property(e => e.cod_prov_sunat)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.Property(e => e.cod_ubigeo_inei)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();

            entity.Property(e => e.cod_ubigeo_reniec)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();

            entity.Property(e => e.cod_ubigeo_sunat)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();

            entity.Property(e => e.desc_dep_inei)
                .HasMaxLength(13)
                .IsUnicode(false);

            entity.Property(e => e.desc_dep_reniec)
                .HasMaxLength(23)
                .IsUnicode(false);

            entity.Property(e => e.desc_dep_sunat)
                .HasMaxLength(23)
                .IsUnicode(false);

            entity.Property(e => e.desc_prov_inei)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.Property(e => e.desc_prov_reniec)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.Property(e => e.desc_prov_sunat)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.Property(e => e.desc_ubigeo_inei)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.desc_ubigeo_reniec)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.desc_ubigeo_sunat)
                .HasMaxLength(36)
                .IsUnicode(false);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<ubigeo> entity);
    }
}
