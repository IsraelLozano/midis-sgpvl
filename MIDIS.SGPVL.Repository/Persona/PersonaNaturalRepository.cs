using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Entity.Models.Persona;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.Persona
{
    public class PersonaNaturalRepository : GenericRepository<VLPerNatural>, IPersonaNaturalRepository
    {
        public PersonaNaturalRepository(BDPVLContext context) : base(context)
        {
        }
    }
}
