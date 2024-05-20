using AutoMapper;
using MIDIS.SGPVL.Entity.Models.ComitePvl;
using MIDIS.SGPVL.Manager.Settings;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Cmd;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Get;
using MIDIS.SGPVL.Repository.UnitOfWork;

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
                    _comiteUnitOfWork._usuarioRepository.Insert(entidad);
                }
                else
                {
                    //var query = _comiteUnitOfWork._comiteAdminRepository.GetById(model.iIdComite);
                    _comiteUnitOfWork._usuarioRepository.Update(entidad);
                }
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
    }
}
