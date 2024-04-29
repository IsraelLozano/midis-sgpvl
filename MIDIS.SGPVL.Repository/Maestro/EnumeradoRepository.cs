using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Entity.Models.Maestro;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.Maestro
{
    public class EnumeradoRepository : GenericRepository<VLEnumerado>, IEnumeradoRepository
    {
        public EnumeradoRepository(BDPVLContext context) : base(context)
        {
        }
    }
}
