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
            CreateMap<MaeDepartamento, GetDptoDto>();
            CreateMap<MaeProvincium, GetProvinciaDto>();
            CreateMap<ubigeo, GetUbigeoDto>();
            CreateMap<VLEnumerado, GetDistritoDto>();

            CreateMap<VLEnumItem, GetEnumeradoComboDto>();
            #endregion

            #region Comite Administrativo

            CreateMap<VLAdministrativo, GetAdministrativoDto>();

            #endregion
        }
    }
}
