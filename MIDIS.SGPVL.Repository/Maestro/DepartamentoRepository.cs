using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Entity.Models.Maestro;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.Maestro
{
    public class DepartamentoRepository : GenericRepository<MaeDepartamento>, IDepartamentoRepository
    {
        public DepartamentoRepository(BDPVLContext context) : base(context)
        {
        }
    }
}
