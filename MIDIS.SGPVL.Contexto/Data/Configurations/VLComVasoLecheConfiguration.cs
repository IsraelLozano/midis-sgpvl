﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIDIS.SGPVL.Entity.Models.ComitePvl;

namespace MIDIS.SGPVL.Contexto.Data.Configurations
{
    public partial class VLComVasoLecheConfiguration : IEntityTypeConfiguration<VLComVasoLeche>
    {
        public void Configure(EntityTypeBuilder<VLComVasoLeche> entity)
        {
            entity.HasKey(e => e.iCodComVasLeche).HasName("PK__VLComVas__B036EB25A48EFA5B");

            entity.ToTable("VLComVasoLeche", "ComitePvl");
            entity.Property(e => e.dFecModifica).HasColumnType("datetime");
            entity.Property(e => e.dFecRegistro).HasColumnType("datetime");
            entity.Property(e => e.vUbigeo).HasMaxLength(30);
            entity.Property(e => e.vCodComite).IsRequired().HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.vDireccion).IsRequired().HasMaxLength(200).IsUnicode(false);
            entity.Property(e => e.vLatitud).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.vLongitud).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.vNomComite).IsRequired().HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.vReferencia).HasMaxLength(200).IsUnicode(false);
            entity.Property(e => e.vUsuModifica).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.vUsuRegistro).HasMaxLength(50).IsUnicode(false);

            entity.HasOne(d => d.iTipAlimentoNavigation)
                .WithMany(p => p.VLComVasoLecheiTipAlimentoNavigations)
                .HasForeignKey(d => d.iTipAlimento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VLComVaso__iTipA__4C364F0E");

            entity.HasOne(d => d.iTipOsbNavigation)
                .WithMany(p => p.VLComVasoLecheiTipOsbNavigations)
                .HasForeignKey(d => d.iTipOsb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VLComVaso__iTipO__4B422AD5");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<VLComVasoLeche> entity);
    }
}
