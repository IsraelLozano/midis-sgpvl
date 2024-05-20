using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MIDIS.SGPVL.Entity.Models.ComitePvl;
using MIDIS.SGPVL.Manager.Settings;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Cmd;
using MIDIS.SGPVL.ManagerDto.ComitePvl.Get;
using MIDIS.SGPVL.Repository.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace MIDIS.SGPVL.Manager.ComitePvl
{
    public class SocioManager : ISocioManager
    {
        private readonly IMapper _mapper;
        private readonly IAplicationConstants _aplicationConstants;
        private readonly ComiteUnitOfWork _comiteUnitOfWork;
        private readonly PersonaUnitOfWork _personaUnitOfWork;

        public SocioManager(IMapper mapper,
            IAplicationConstants aplicationConstants,
            ComiteUnitOfWork comiteUnitOfWork,
            PersonaUnitOfWork personaUnitOfWork)
        {
            _mapper = mapper;
            _aplicationConstants = aplicationConstants;
            _comiteUnitOfWork = comiteUnitOfWork;
            _personaUnitOfWork = personaUnitOfWork;
        }

        public async Task<List<GetSocioDto>> GetListSocioByComiteAsync(int idComite)
        {
            var response = new List<GetSocioDto>();
            var query = _comiteUnitOfWork
                ._socioReposiroty
                .GetAll(l => l.iCodComVasLeche == idComite,
                includeProperties: "iTipSocioNavigation,iCodPersonaNavigation.iTipDocumentoNavigation");

            return _mapper.Map<List<GetSocioDto>>(query);
        }

        public async Task<CmdSocioDto> GetSocioByIdAsync(int id)
        {
            var query = _comiteUnitOfWork._socioReposiroty.GetAll(l => l.iIdSocio == id).FirstOrDefault();
            var response = _mapper.Map<CmdSocioDto>(query);
            return response;
        }

        public async Task<CmdSocioDto> AddSocioAsync(CmdSocioDto model)
        {
            var entidad = _mapper.Map<VLSocio>(model);
            try
            {
                if (entidad.iIdSocio == 0)
                {
                    entidad.dFecRegistro = entidad.dFecModifica = DateTime.Now;
                    entidad.vUsuRegistro = entidad.vUsuModifica = _aplicationConstants.UsuarioSesionBE.Credenciales;
                    _comiteUnitOfWork._socioReposiroty.Insert(entidad);
                }
                else
                {
                    //var query = _comiteUnitOfWork._comiteAdminRepository.GetById(model.iIdComite);
                    _comiteUnitOfWork._socioReposiroty.Update(entidad);
                }
                await _comiteUnitOfWork.SaveAsync();
                return _mapper.Map<CmdSocioDto>(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteSocioAsync(int id)
        {

            var entity = _comiteUnitOfWork._socioReposiroty.GetById(id);
            entity.bActivo = false;
            return (await _comiteUnitOfWork.SaveAsync()) == 1;
        }
    }
}
