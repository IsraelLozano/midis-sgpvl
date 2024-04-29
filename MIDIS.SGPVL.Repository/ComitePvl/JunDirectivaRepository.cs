using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Entity.Models.ComitePvl;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.ComitePvl
{
    public class JunDirectivaRepository : GenericRepository<VLJunDirectiva>, IJunDirectivaRepository
    {
        public JunDirectivaRepository(BDPVLContext context) : base(context)
        {
        }
    }
}
