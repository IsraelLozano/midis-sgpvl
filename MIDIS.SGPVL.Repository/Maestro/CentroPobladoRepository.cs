using Microsoft.EntityFrameworkCore;
using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Entity.Models.Maestro;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.Maestro
{
    public class CentroPobladoRepository : GenericRepository<MaeCentroPoblado>, ICentroPobladoRepository
    {
        public CentroPobladoRepository(BDPVLContext context) : base(context)
        {
        }
    }
}
