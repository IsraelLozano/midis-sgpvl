using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Entity.Models.ComitePvl;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.ComitePvl
{
    public class ComitePVLRepository : GenericRepository<VLComVasoLeche>, IComitePVLRepository
    {
        public ComitePVLRepository(BDPVLContext context) : base(context)
        {
        }
    }
}
