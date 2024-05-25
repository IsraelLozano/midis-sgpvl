using AutoMapper;
using MIDIS.SGPVL.Entity.Models.ComitePvl;
using MIDIS.SGPVL.Entity.Models.Persona;
using MIDIS.SGPVL.Manager.Settings;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Cmd;
using MIDIS.SGPVL.Repository.UnitOfWork;
using MIDIS.SGPVL.Utils.Enumerados;

namespace MIDIS.SGPVL.Manager.ComitePvl
{
    public class BeneficiarioManager : IBeneficiarioManager
    {
        private readonly IMapper _mapper;
        private readonly IAplicationConstants _aplicationConstants;
        private readonly ComiteUnitOfWork _comiteUnitOfWork;
        private readonly PersonaUnitOfWork _personaUnitOfWork;

        public BeneficiarioManager(IMapper mapper,
            IAplicationConstants aplicationConstants,
            ComiteUnitOfWork comiteUnitOfWork,
            PersonaUnitOfWork personaUnitOfWork)
        {
            _mapper = mapper;
            _aplicationConstants = aplicationConstants;
            _comiteUnitOfWork = comiteUnitOfWork;
            _personaUnitOfWork = personaUnitOfWork;
        }

        public async Task<List<CmdBeneficiarioDto>> GetListBeneficiarioByComiteAsync(int idComite)
        {
            var response = new List<CmdBeneficiarioDto>();
            var query = _comiteUnitOfWork
                ._usuarioRepository
                .GetAll(l => l.iCodComVasLeche == idComite,
                includeProperties: "iTipBeneficiarioNavigation,iCodPersonaNavigation.iTipDocumentoNavigation");

            return _mapper.Map<List<CmdBeneficiarioDto>>(query);
        }

        public async Task<CmdBeneficiarioDto> GetBeneficiarioByIdAsync(int id)
        {
            var query = _comiteUnitOfWork._usuarioRepository.GetAll(l => l.iIdUsuario == id).FirstOrDefault();
            var response = _mapper.Map<CmdBeneficiarioDto>(query);
            return response;
        }

        public async Task<CmdBeneficiarioDto> AddBeneficiarioAsync(CmdBeneficiarioDto model)
        {
            var entidad = _mapper.Map<VLUsuario>(model);
            try
            {
                if (entidad.iIdUsuario == 0)
                {
                    entidad.dFecRegistro = entidad.dFecModifica = DateTime.Now;
                    entidad.vUsuRegistro = entidad.vUsuModifica = _aplicationConstants.UsuarioSesionBE.Credenciales;
                    entidad.bActivo = true;
                    
                    var persona = getPersona(model);

                    _personaUnitOfWork._personaRepository.Insert(persona);

                    await _personaUnitOfWork.SaveAsync();

                    entidad.iCodPersona = persona.iCodPersona;

                    _comiteUnitOfWork._usuarioRepository.Insert(entidad);
                }
                else
                {
                    //var query = _comiteUnitOfWork._comiteAdminRepository.GetById(model.iIdComite);
                    _comiteUnitOfWork._usuarioRepository.Update(entidad);
                }

                await _comiteUnitOfWork.SaveAsync();

                var comite = _comiteUnitOfWork._comitePVLRepository.GetById(model.iCodComVasLeche);

                comite.iNumSocio = _comiteUnitOfWork._usuarioRepository.GetAll(l => l.iCodComVasLeche == model.iCodComVasLeche).Count;

                _comiteUnitOfWork._comitePVLRepository.Update(comite);

                await _comiteUnitOfWork.SaveAsync();

                return _mapper.Map<CmdBeneficiarioDto>(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteBeneficiarioAsync(int id)
        {
            var entity = _comiteUnitOfWork._usuarioRepository.GetById(id);
            entity.bActivo = false;
            return (await _comiteUnitOfWork.SaveAsync()) == 1;
        }

        private VLPersona getPersona(CmdBeneficiarioDto model)
        {
            var persona = new VLPersona
            {
                iTipPersona = (int)EnumTipoPersona.natural,
                vNomComercial = $"{model.vApePaterno} {model.vApeMaterno}, {model.vNombre}",
                vNomCompleto = $"{model.vApePaterno} {model.vApeMaterno}, {model.vNombre}",
                vRuc = string.Empty,
                bEstado = true
            };
            persona.VLPerNatural = new VLPerNatural
            {
                vApePaterno = model.vApePaterno,
                vApeMaterno = model.vApeMaterno,
                vNombre = model.vNombre,
                vNroDocumento = model.vNroDocumento,
                bActivo = true,
                iTipDocumento = model.iTipDocumento,
                bSexo = model.bSexo,
                vTelFijo = model.vTelFijo,
                vCelular = model.vCelular,
                vEmail = model.vEmail,
                vDireccion = model.vDireccion,
                vUbigeo = string.Empty,
                dFecNacimiento = null
            };

            return persona;

        }
    }
}
