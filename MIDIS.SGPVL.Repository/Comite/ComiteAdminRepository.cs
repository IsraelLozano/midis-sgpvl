using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Entity.Models.Comite;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.Comite
{
    public class ComiteAdminRepository : GenericRepository<VLAdministrativo>,IComiteAdminRepository
    {
        public ComiteAdminRepository(BDPVLContext context) : base(context)
        {

        }
    }
}
