using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Entity.Models.Comite;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.Comite
{
    public class ComiteAdminMiembroRepository : GenericRepository<VLAdmMiembro>, IComiteAdminMiembroRepository
    {
        public ComiteAdminMiembroRepository(BDPVLContext context) : base(context)
        {
        }
    }
}
