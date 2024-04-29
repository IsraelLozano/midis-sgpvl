using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Entity.Models.ComitePvl;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.ComitePvl
{
    public class SocioReposiroty : GenericRepository<VLSocio>, ISocioReposiroty
    {
        public SocioReposiroty(BDPVLContext context) : base(context)
        {
        }
    }
}
