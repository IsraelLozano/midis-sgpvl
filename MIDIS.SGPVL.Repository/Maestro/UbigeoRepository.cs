using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Entity.Models.Maestro;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.Maestro
{
    public class UbigeoRepository : GenericRepository<ubigeo>, IUbigeoRepository
    {
        public UbigeoRepository(BDPVLContext context) : base(context)
        {

        }
    }
}
