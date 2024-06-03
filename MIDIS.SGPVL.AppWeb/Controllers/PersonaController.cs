using Microsoft.AspNetCore.Mvc;
using MIDIS.SGPVL.Manager.Settings;

namespace MIDIS.SGPVL.AppWeb.Controllers
{
    public class PersonaController : Controller
    {
        private readonly ILogger<PersonaController> _logger;
        private readonly IAplicationConstants _aplicationConstants;

        public PersonaController(ILogger<PersonaController> logger, IAplicationConstants aplicationConstants)
        {
            _logger = logger;
            _aplicationConstants = aplicationConstants;
        }

        //public async Task<IActionResult> getPersona(int tipoDoc, string nroDoc)
        //{
           
        //}
    }
}
