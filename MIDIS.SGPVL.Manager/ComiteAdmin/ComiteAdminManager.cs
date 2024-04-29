
using AutoMapper;
using MIDIS.SGPVL.Manager.Maestro;
using MIDIS.SGPVL.ManagerDto.Maestro.Get;
using MIDIS.SGPVL.Repository.UnitOfWork;

namespace MIDIS.SGPVL.Manager.ComiteAdmin
{
    public class ComiteAdminManager
    {

        private readonly IMapper _mapper;
        private readonly MaestroUnitOfWork _maestraUnitOfWork;
        private readonly IMaestroManager _maestraManager;
        private readonly PersonaUnitOfWork _personaUnitOfWork;

        public ComiteAdminManager(IMapper mapper, MaestroUnitOfWork maestraUnitOfWork, IMaestroManager maestraManager, PersonaUnitOfWork personaUnitOfWork)
        {
            _mapper = mapper;
            _maestraUnitOfWork = maestraUnitOfWork;
            _maestraManager = maestraManager;
            _personaUnitOfWork = personaUnitOfWork;
        }


        public Task<List<GetEnumeradoComboDto>> GetAdministrativo(string filter)
        {
            return null;
        }
        
        //public Task<List<GetEnumeradoComboDto>> GetAdministrativo(string filter)
        //{
        //    return;
        //}



    }
}
