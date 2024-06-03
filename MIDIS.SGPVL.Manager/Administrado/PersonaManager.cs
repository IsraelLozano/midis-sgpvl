using AutoMapper;
using MIDIS.SGPVL.Manager.AgenteExterno;
using MIDIS.SGPVL.Manager.Settings;
using MIDIS.SGPVL.ManagerDto.Persona;
using MIDIS.SGPVL.Repository.UnitOfWork;
using MIDIS.SGPVL.Utils.Enumerados;

namespace MIDIS.SGPVL.Manager.Administrado
{
    public class PersonaManager
    {
        private readonly IMapper _mapper;
        private readonly IAplicationConstants _aplicationConstants;
        private readonly PersonaUnitOfWork _personaUnitOfWork;
        private readonly IAgentExternalManager _agentExternalManager;

        public PersonaManager(IMapper mapper,
            IAplicationConstants aplicationConstants,
            PersonaUnitOfWork personaUnitOfWork,
            IAgentExternalManager agentExternalManager)
        {
            _mapper = mapper;
            _aplicationConstants = aplicationConstants;
            _personaUnitOfWork = personaUnitOfWork;
            _agentExternalManager = agentExternalManager;
        }

        //public async Task<GetPersonaNaturalDto> GetPersonaByDocumento(int tipoDoc, string nroDoc)
        //{

        //    var query = _personaUnitOfWork._personaNaturalRepository.GetAll(l => l.iTipDocumento == tipoDoc && l.vNroDocumento == nroDoc).FirstOrDefault();


        //    if (query == null)
        //    {
        //        if (tipoDoc == (int)EnumTipoDocumento.dni)
        //        {
        //            var response = await _agentExternalManager.GetPersonaReniec(nroDoc);
        //        }
        //    }
        //}
    }
}
