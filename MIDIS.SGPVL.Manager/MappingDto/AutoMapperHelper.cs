using AutoMapper;
using MIDIS.SGPVL.Entity.Models.Comite;
using MIDIS.SGPVL.Entity.Models.ComitePvl;
using MIDIS.SGPVL.Entity.Models.Maestro;
using MIDIS.SGPVL.Entity.Models.Persona;
using MIDIS.SGPVL.ManagerDto.ComiteAdmin.Cmd;
using MIDIS.SGPVL.ManagerDto.ComiteAdmin.Get;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Cmd;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Get;
using MIDIS.SGPVL.ManagerDto.Maestro.Get;
using MIDIS.SGPVL.ManagerDto.Persona;

namespace MIDIS.SGPVL.Manager.MappingDto
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            #region Maestros
            CreateMap<MaeDistrito, GetDistritoDto>();

            CreateMap<MaeDepartamento, GetDptoDto>()
                 .ForMember(dest => dest.id, source => source.MapFrom(s => s.vCodigoDepartamento))
                 .ForMember(dest => dest.descripcion, source => source.MapFrom(s => s.vDescripcion));

            CreateMap<MaeProvincium, GetProvinciaDto>()
                .ForMember(dest => dest.id, source => source.MapFrom(s => s.vCodigoProvincia))
                .ForMember(dest => dest.Descripcion, source => source.MapFrom(s => s.vDescripcion));

            CreateMap<ubigeo, GetUbigeoDto>()
                .ForMember(dest => dest.codUbigeo, source => source.MapFrom(s => s.cod_ubigeo_inei))
                .ForMember(dest => dest.descripcion, source => source.MapFrom(s => s.desc_ubigeo_inei));


            CreateMap<MaeDistrito, GetDistritoDto>()
                .ForMember(dest => dest.codigo, source => source.MapFrom(s => $"{s.vCodigoDepartamento}{s.vCodigoProvincia}{s.vCodigoDistrito}"))
                .ForMember(dest => dest.descripcion, source => source.MapFrom(s => s.vDescripcion))
                .ForMember(dest => dest.provincia, source => source.MapFrom(s => s.NombreProvincia))
                .ForMember(dest => dest.dpto, source => source.MapFrom(s => s.NombreDpto));

            CreateMap<MaeCentroPoblado, GetCentroPobladoDto>()
                .ForMember(dest => dest.codigo, source => source.MapFrom(s => s.vUbigeo))
                .ForMember(dest => dest.descripcion, source => source.MapFrom(s => s.vCentroPoblado));

            CreateMap<VLEnumItem, GetEnumeradoComboDto>()
                .ForMember(dest => dest.id, source => source.MapFrom(s => s.iIdEnuItem))
                .ForMember(dest => dest.descripcion, source => source.MapFrom(s => s.vDescripcion));
            #endregion

            #region Comite Administrativo

            CreateMap<VLAdministrativo, GetAdministrativoDto>();
            CreateMap<VLAdministrativo, CmdComiteAdminDto>().ReverseMap();

            CreateMap<VLAdmMiembro, CmdComiteMemberAdminDto>()
                 .ForMember(dest => dest.vApePaterno, source => source.MapFrom(s => s.iCodPersonaNavigation.vApePaterno))
                 .ForMember(dest => dest.vApeMaterno, source => source.MapFrom(s => s.iCodPersonaNavigation.vApeMaterno))
                 .ForMember(dest => dest.vNombre, source => source.MapFrom(s => s.iCodPersonaNavigation.vNombre))
                 .ForMember(dest => dest.vNroDocumento, source => source.MapFrom(s => s.iCodPersonaNavigation.vNroDocumento))
                 .ForMember(dest => dest.iTipDocumento, source => source.MapFrom(s => s.iCodPersonaNavigation.iTipDocumento))
                 .ForMember(dest => dest.bSexo, source => source.MapFrom(s => s.iCodPersonaNavigation.bSexo))
                 .ForMember(dest => dest.vTelFijo, source => source.MapFrom(s => s.iCodPersonaNavigation.vTelFijo))
                 .ForMember(dest => dest.vCelular, source => source.MapFrom(s => s.iCodPersonaNavigation.vCelular))
                 .ForMember(dest => dest.vEmail, source => source.MapFrom(s => s.iCodPersonaNavigation.vEmail))
                 .ForMember(dest => dest.vDireccion, source => source.MapFrom(s => s.iCodPersonaNavigation.vDireccion));

            CreateMap<CmdComiteMemberAdminDto, VLAdmMiembro>();
            CreateMap<VLAdmMiembro, GetAdminMiembroDto>().ReverseMap();

            CreateMap<VLPerNatural, GetPersonaNaturalDto>().ReverseMap();

            #endregion

            #region Comite PVL

            CreateMap<VLComVasoLeche, GetComiteDto>();
            CreateMap<VLJunDirectiva, GetComiteJdDto>();
            CreateMap<VLMiembroJuntum, GetMiembroJdDto>();
            CreateMap<VLUsuario, GetBeneficiarioDto>();

            //CMD
            CreateMap<VLComVasoLeche, CmdComiteDto>().ReverseMap();
            CreateMap<VLJunDirectiva, CmdComiteJdDto>().ReverseMap();
            CreateMap<VLMiembroJuntum, CmdMiembroJdDto>().ReverseMap();


            //Socios
            CreateMap<VLSocio, GetSocioDto>();
            CreateMap<VLSocio, CmdSocioDto>().ReverseMap();


            //Usuario

            CreateMap<VLUsuario, GetBeneficiarioDto>()
                .ForMember(dest => dest.fullNameSocio, source => source.MapFrom(s =>
                $"{s.iIdSocioNavigation.iCodPersonaNavigation.vApePaterno} {s.iIdSocioNavigation.iCodPersonaNavigation.vApeMaterno}, {s.iIdSocioNavigation.iCodPersonaNavigation.vNombre}"

                ))
                .ForMember(dest => dest.NroDocumentoSocio, source => source.MapFrom(s =>
                $"{s.iIdSocioNavigation.iCodPersonaNavigation.iTipDocumentoNavigation.vDescripcion}: " +
                $"{s.iIdSocioNavigation.iCodPersonaNavigation.vNroDocumento}"
                ))
                .ForMember(dest => dest.fullNameUsuario, source => source.MapFrom(s =>
                $"{s.iCodPersonaNavigation.vApePaterno} {s.iCodPersonaNavigation.vApeMaterno}, {s.iCodPersonaNavigation.vNombre}"

                ))
                .ForMember(dest => dest.NroDocUsuario, source => source.MapFrom(s =>
                $"{s.iCodPersonaNavigation.iTipDocumentoNavigation.vDescripcion} :" +
                $"{s.iCodPersonaNavigation.vNroDocumento}"
                ))
                .ForMember(dest => dest.fechaNacimiento, source => source.MapFrom(s => s.iCodPersonaNavigation.dFecNacimiento.Value.ToShortDateString()
                ))

                .ForMember(dest => dest.edad, source => source.MapFrom(s =>
                        Math.Round(((DateTime.Now - s.iCodPersonaNavigation.dFecNacimiento.Value).TotalDays / 365.25D), 0, MidpointRounding.ToZero)
                ))
                ;
            CreateMap<VLUsuario, CmdBeneficiarioDto>().ReverseMap();



            #endregion
        }
    }
}
