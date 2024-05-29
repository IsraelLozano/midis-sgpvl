using AutoMapper;
using MIDIS.SGPVL.Manager.Settings;
using MIDIS.SGPVL.Utils.Dtos;
using System.ServiceModel;
using WsReniec;

namespace MIDIS.SGPVL.Manager.AgenteExterno
{
    public class AgentExternalManager : IAgentExternalManager
    {
        private readonly IMapper _mapper;
        private readonly IAplicationConstants _aplicationConstants;
        private readonly UrlServiceAgent _urlServiceAgent;

        //public AgentExternalManager(IMapper mapper, IAplicationConstants aplicationConstants, UrlServiceAgent urlServiceAgent)
        //{
        //    _mapper = mapper;
        //    _aplicationConstants = aplicationConstants;
        //    _urlServiceAgent = urlServiceAgent;
        //}

        public async Task<ReniecPersona_Registro> GetPersonaReniec(string nroDocumento)
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            var address = new EndpointAddress("http://app_pruebas.midis.gob.pe/Sis_WS/App/ReniecPersona_Servicio.svc");

            ReniecPersona_Request credentials = new ReniecPersona_Request()
            {
                NumeroDeDocumento = nroDocumento,
                Usuario = "APP15",//_urlServiceAgent.User,
                Clave = "P@55W0RD"//_urlServiceAgent.Password,
            };

            WsReniec.ConsultarPorNumeroDeDocumentoRequest request = new ConsultarPorNumeroDeDocumentoRequest(credentials);

            var factory = new ChannelFactory<IReniecPersona_Servicio>(basicHttpBinding, address);
            IReniecPersona_Servicio channel = factory.CreateChannel();

            ConsultarPorNumeroDeDocumentoResponse response = await channel.ConsultarPorNumeroDeDocumentoAsync(request);

            var person = response.ConsultarPorNumeroDeDocumentoResult.PersonaDato;

            return person;
        }
    }
}
