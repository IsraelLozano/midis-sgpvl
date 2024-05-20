using AutoMapper;
using MIDIS.SGPVL.Entity.Models.ComitePvl;
using MIDIS.SGPVL.Entity.Models.Persona;
using MIDIS.SGPVL.Manager.Maestro;
using MIDIS.SGPVL.Manager.Settings;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Cmd;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Get;
using MIDIS.SGPVL.Repository.UnitOfWork;
using MIDIS.SGPVL.Utils.Dtos;
using MIDIS.SGPVL.Utils.Enumerados;
using MIDIS.SGPVL.Utils.Helpers.FileManager;

namespace MIDIS.SGPVL.Manager.ComitePvl
{
    public class JuntaDirectivaManager : IJuntaDirectivaManager
    {
        private readonly IMapper _mapper;
        private readonly MaestroUnitOfWork _maestraUnitOfWork;
        private readonly IMaestroManager _maestraManager;
        private readonly IAplicationConstants _aplicationConstants;
        private readonly ComiteUnitOfWork _comiteUnitOfWork;
        private readonly ResourceDto _resourceDto;
        private readonly IStorageManager _storageManager;
        private readonly PersonaUnitOfWork _personaUnitOfWork;

        public JuntaDirectivaManager(IMapper mapper,
            MaestroUnitOfWork maestraUnitOfWork,
            IMaestroManager maestraManager,
            IAplicationConstants aplicationConstants,
            ComiteUnitOfWork comiteUnitOfWork,
            ResourceDto resourceDto,
            IStorageManager storageManager,
            PersonaUnitOfWork personaUnitOfWork)
        {
            _mapper = mapper;
            _maestraUnitOfWork = maestraUnitOfWork;
            _maestraManager = maestraManager;
            _aplicationConstants = aplicationConstants;
            _comiteUnitOfWork = comiteUnitOfWork;
            _resourceDto = resourceDto;
            _storageManager = storageManager;
            _personaUnitOfWork = personaUnitOfWork;
        }

        public async Task<List<GetComiteJdDto>> GetJuntaDirectivaByComiteAsync(int idComite)
        {
            var response = new List<GetComiteJdDto>();
            var query = _comiteUnitOfWork
                ._junDirectivaRepository
                .GetAll(l => l.iCodComVasLeche == idComite,
                includeProperties: "iTipResolucionNavigation,VLMiembroJunta.iCodPersonaNavigation.iTipDocumentoNavigation,VLMiembroJunta.iTipCargoNavigation");

            return _mapper.Map<List<GetComiteJdDto>>(query);

        }

        public async Task<GetComiteJdDto> GetJuntaByIdAsync(int id)
        {
            var query = _comiteUnitOfWork._junDirectivaRepository.GetAll(l => l.iIdJunta == id, includeProperties: "iTipResolucionNavigation").FirstOrDefault();
            var response = _mapper.Map<GetComiteJdDto>(query);
            return response;
        }

        public async Task<CmdComiteJdDto> AddComiteAdmin(CmdComiteJdDto model)
        {
            var entidad = _mapper.Map<VLJunDirectiva>(model);
            try
            {
                var rutaCarpeta = $"{model.iCodComVasLeche}-{model.vNumResolucion.Trim()}";
                var fileNameSave = string.Format("file_{0}.pdf", DateTime.Now.ToString("yyyyMMddTHHmmss"));

                if (entidad.iIdJunta == 0)
                {
                    entidad.dFecRegistro = entidad.dFecModifica = DateTime.Now;
                    entidad.vUsuRegistro = entidad.vUsuModifica = _aplicationConstants.UsuarioSesionBE.Credenciales;
                    entidad.bVigente = true;
                    entidad.vNomArcGuid = Path.Combine(rutaCarpeta, fileNameSave);
                    entidad.iNumMiembro = 0;
                    //Guid.NewGuid().ToString();
                    entidad.vNomArchivo = model.FileResol.FileName;

                    _comiteUnitOfWork._junDirectivaRepository.Insert(entidad);
                }
                else
                {
                    //var query = _comiteUnitOfWork._comiteAdminRepository.GetById(model.iIdComite);
                    _comiteUnitOfWork._junDirectivaRepository.Update(entidad);
                }
                var result = await _comiteUnitOfWork.SaveAsync() != 0;

                if (model.iIdJunta == 0)
                {
                    var pathToSave = Path.Combine(_resourceDto.JundaDirectiva, rutaCarpeta);
                    await _storageManager.SaveFileFormCollection(pathToSave, fileNameSave, model.FileResol);
                }

                return _mapper.Map<CmdComiteJdDto>(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Miembro-junta

        public async Task<List<GetMiembroJdDto>> GetMiembrosByJuntaAsync(int idJunta)
        {
            var query = _comiteUnitOfWork
           ._miembroJuntaRepository
           .GetAll(l => l.iIdJunta == idJunta, includeProperties: "iTipCargoNavigation,iCodPersonaNavigation.iTipDocumentoNavigation");
            var resp = _mapper.Map<List<GetMiembroJdDto>>(query);
            return resp;
        }

        public async Task<CmdMiembroJdDto> AddComiteMemberAdmin(CmdMiembroJdDto model)
        {
            var entidad = _mapper.Map<VLMiembroJuntum>(model);
            try
            {
                if (entidad.iIdMiembro == 0)
                {
                    entidad.dFecRegistro = entidad.dFecModifica = DateTime.Now;
                    entidad.vUsuRegistro = entidad.vUsuModifica = _aplicationConstants.UsuarioSesionBE.Credenciales;
                    entidad.bActivo = true;

                    var persona = getPersona(model);

                    _personaUnitOfWork._personaRepository.Insert(persona);

                    await _personaUnitOfWork.SaveAsync();

                    entidad.iCodPersona = persona.iCodPersona;

                    _comiteUnitOfWork._miembroJuntaRepository.Insert(entidad);

                }
                else
                {
                    //_administrativoUnitOfWork._comiteAdminRepository.Update(entidad);
                }

                await _comiteUnitOfWork.SaveAsync();

                //Actualizar nro Miembros 
                var comite = _comiteUnitOfWork._junDirectivaRepository.GetById(model.iIdJunta);

                comite.iNumMiembro = _comiteUnitOfWork._miembroJuntaRepository.GetAll(l => l.iIdJunta == model.iIdJunta).Count;

                _comiteUnitOfWork._junDirectivaRepository.Update(comite);

                await _comiteUnitOfWork.SaveAsync();

                return _mapper.Map<CmdMiembroJdDto>(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private VLPersona getPersona(CmdMiembroJdDto model)
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

        #endregion
    }
}
