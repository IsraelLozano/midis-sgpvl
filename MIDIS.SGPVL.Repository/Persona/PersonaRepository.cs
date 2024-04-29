using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Entity.Models.Persona;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.Persona
{
    public class PersonaRepository : GenericRepository<VLPersona>, IPersonaRepository
    {
        public PersonaRepository(BDPVLContext context) : base(context)
        {
        }
    }
}
