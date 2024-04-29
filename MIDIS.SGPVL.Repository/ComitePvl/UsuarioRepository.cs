using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Entity.Models.ComitePvl;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.ComitePvl
{
    public class UsuarioRepository : GenericRepository<VLUsuario>, IUsuarioRepository
    {
        public UsuarioRepository(BDPVLContext context) : base(context)
        {
        }
    }
}
