
using AutoMapper;
using MIDIS.SGPVL.Manager.Maestro;
using MIDIS.SGPVL.ManagerDto.ComiteAdmin.Get;
using MIDIS.SGPVL.ManagerDto.Maestro.Get;
using MIDIS.SGPVL.Repository.UnitOfWork;

namespace MIDIS.SGPVL.Manager.ComiteAdmin
{
    public class ComiteAdminManager : IComiteAdminManager
    {

        private readonly IMapper _mapper;
        private readonly MaestroUnitOfWork _maestraUnitOfWork;
        private readonly IMaestroManager _maestraManager;
        private readonly PersonaUnitOfWork _personaUnitOfWork;
        private readonly AdministrativoUnitOfWork _administrativoUnitOfWork;

        public ComiteAdminManager(IMapper mapper, MaestroUnitOfWork maestraUnitOfWork, IMaestroManager maestraManager, PersonaUnitOfWork personaUnitOfWork, AdministrativoUnitOfWork administrativoUnitOfWork)
        {
            _mapper = mapper;
            _maestraUnitOfWork = maestraUnitOfWork;
            _maestraManager = maestraManager;
            _personaUnitOfWork = personaUnitOfWork;
            _administrativoUnitOfWork = administrativoUnitOfWork;
        }


        public async Task<List<GetAdministrativoDto>> GetAdministrativo(string filter)
        {
            var query = _administrativoUnitOfWork._comiteAdminRepository.GetAll(includeProperties: "iTipResolucionNavigation");
            var response = _mapper.Map<List<GetAdministrativoDto>>(query);
            return response;

        }

        public async Task<GetAdministrativoFiltersDto> GetAdministrativoFilters(string filter)
        {
            var response = new GetAdministrativoFiltersDto();
            response.dptos = _mapper.Map<List<GetDptoDto>>(_maestraUnitOfWork._departamentoRepository.GetAll());
            response.provincias = _mapper.Map<List<GetProvinciaDto>>(_maestraUnitOfWork._provinciaRepository.GetAll());

            return response;

        }



    }
}
