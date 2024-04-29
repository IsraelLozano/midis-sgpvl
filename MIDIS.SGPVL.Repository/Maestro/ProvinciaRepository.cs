using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Entity.Models.Maestro;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.Maestro
{
    public class ProvinciaRepository : GenericRepository<MaeProvincium>, IProvinciaRepository
    {
        public ProvinciaRepository(BDPVLContext context) : base(context)
        {
        }
    }
}
