using AutoMapper;
using MIDIS.SGPVL.Manager.AgenteExterno;
using MIDIS.SGPVL.Manager.Settings;
using MIDIS.SGPVL.Utils.Dtos;
using MIDIS.SGPVL.Utils.Helpers.Dapper;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MIDIS.SGPVL.Test
{
    public class AgentExternalManagerTest: IClassFixture<AgentExternalManager>
    {

        private readonly ITestOutputHelper _output;
        private readonly AgentExternalManager _agentExternalManager;

        public AgentExternalManagerTest(ITestOutputHelper output, AgentExternalManager agentExternalManager)
        {
            _output = output;
            _agentExternalManager = agentExternalManager;
        }


        //private readonly IMapper _mapper;
        //private readonly IAplicationConstants _aplicationConstants;
        //private readonly UrlServiceAgent _urlServiceAgent;




        [Fact]
        public async Task GetAgentExternalManager()
        {
            var dni = "42742732";
            var result = await _agentExternalManager.GetPersonaReniec(dni);
            Assert.True(string.IsNullOrEmpty(result.ApellidoPaterno), $"Su clave secreta es: {result.ApellidoPaterno} ");
        }

    }
}
