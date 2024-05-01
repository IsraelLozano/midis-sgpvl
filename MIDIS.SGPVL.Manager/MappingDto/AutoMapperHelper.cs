using AutoMapper;
using MIDIS.SGPVL.Entity.Models.Comite;
using MIDIS.SGPVL.Entity.Models.Maestro;
using MIDIS.SGPVL.ManagerDto.ComiteAdmin.Get;
using MIDIS.SGPVL.ManagerDto.Maestro.Get;

namespace MIDIS.SGPVL.Manager.MappingDto
{
    public class AutoMapperHelper: Profile
    {
        public AutoMapperHelper()
        {
            #region Maestros
            CreateMap<MaeDistrito, GetDistritoDto>();
            CreateMap<MaeDepartamento, GetDptoDto>()
                 .ForMember(dest => dest.id, source => source.MapFrom(s => s.vDescripcion));
            CreateMap<MaeProvincium, GetProvinciaDto>()
                .ForMember(dest => dest.id, source => source.MapFrom(s => s.vDescripcion));
            CreateMap<ubigeo, GetUbigeoDto>()
                .ForMember(dest => dest.codUbigeo, source => source.MapFrom(s => s.cod_ubigeo_inei))
                .ForMember(dest => dest.descripcion, source => source.MapFrom(s => s.desc_ubigeo_inei));

            CreateMap<VLEnumerado, GetDistritoDto>();

            CreateMap<VLEnumItem, GetEnumeradoComboDto>()
                .ForMember(dest => dest.id, source => source.MapFrom(s => s.iIdEnuItem))
                .ForMember(dest => dest.descripcion , source => source.MapFrom(s => s.vDescripcion));
            #endregion

            #region Comite Administrativo

            CreateMap<VLAdministrativo, GetAdministrativoDto>();

            #endregion
        }
    }
}
