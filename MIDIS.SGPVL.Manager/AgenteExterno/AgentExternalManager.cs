using AutoMapper;
using MIDIS.SGPVL.Manager.Settings;
using MIDIS.SGPVL.Utils.Dtos;
using System.ServiceModel;
using WsMigracion;
using WsReniec;

namespace MIDIS.SGPVL.Manager.AgenteExterno
{
    public class AgentExternalManager : IAgentExternalManager
    {
        private readonly IMapper _mapper;
        private readonly IAplicationConstants _aplicationConstants;
        private readonly UrlServiceAgent _urlServiceAgent;

        public AgentExternalManager(IMapper mapper, IAplicationConstants aplicationConstants, UrlServiceAgent urlServiceAgent)
        {
            _mapper = mapper;
            _aplicationConstants = aplicationConstants;
            _urlServiceAgent = urlServiceAgent;
        }

        public async Task<ReniecPersona_Registro> GetPersonaReniec(string nroDocumento)
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            var address = new EndpointAddress(_urlServiceAgent.WsReniec);

            ReniecPersona_Request credentials = new ReniecPersona_Request()
            {
                NumeroDeDocumento = nroDocumento,
                Usuario = _urlServiceAgent.User,
                Clave = _urlServiceAgent.Password,
            };

            WsReniec.ConsultarPorNumeroDeDocumentoRequest request = new WsReniec.ConsultarPorNumeroDeDocumentoRequest(credentials);

            var factory = new ChannelFactory<IReniecPersona_Servicio>(basicHttpBinding, address);
            IReniecPersona_Servicio channel = factory.CreateChannel();

            WsReniec.ConsultarPorNumeroDeDocumentoResponse response = await channel.ConsultarPorNumeroDeDocumentoAsync(request);

            var person = response.ConsultarPorNumeroDeDocumentoResult.PersonaDato;

            return person;
        }
        
        public async Task<MigracionesCE_Registro> GetPersonaMigraciones(string nroDocumento)
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            var address = new EndpointAddress(_urlServiceAgent.WsMigracion);

            MigracionesCE_Request credentials = new MigracionesCE_Request()
            {
                NumeroDeDocumento = nroDocumento,
                Usuario = _urlServiceAgent.User,
                Clave = _urlServiceAgent.Password,
            };

            WsMigracion.ConsultarPorNumeroDeDocumentoRequest request = new WsMigracion.ConsultarPorNumeroDeDocumentoRequest(credentials);

            var factory = new ChannelFactory<IMigracionesCE_Servicio>(basicHttpBinding, address);
            IMigracionesCE_Servicio channel = factory.CreateChannel();

            WsMigracion.ConsultarPorNumeroDeDocumentoResponse response = await channel.ConsultarPorNumeroDeDocumentoAsync(request);

            var person = response.ConsultarPorNumeroDeDocumentoResult.CEDato;

            return person;
        }
    }
}
