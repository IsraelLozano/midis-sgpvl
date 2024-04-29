using Microsoft.EntityFrameworkCore;
using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Entity.Models.Maestro;
using MIDIS.SGPVL.Utils.Enumerados;
using PROMART.SISTRAN.Repository.Base;

namespace MIDIS.SGPVL.Repository.Maestro
{
    public class EnumeradoItemRepository : GenericRepository<VLEnumItem>, IEnumeradoItemRepository
    {
        private readonly BDPVLContext _context;

        public EnumeradoItemRepository(BDPVLContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Dictionary<EnumeradoCabecera, List<VLEnumItem>>> GetListEnumeradoByGrupo(List<EnumeradoCabecera> listaMaestra)
        {
            var listaEntera = listaMaestra.Select(x => (int)x).ToList();

            var lista = await _context.VLEnumItems.Where(p => listaEntera.Contains(p.iIdEnuItem)).OrderBy(l => l.vDescripcion).ToListAsync();

            Dictionary<EnumeradoCabecera, List<VLEnumItem>> listaRes = new Dictionary<EnumeradoCabecera, List<VLEnumItem>>();

            foreach (EnumeradoCabecera item in listaMaestra)
            {
                listaRes.Add(item, (from x in lista
                                    where x.iIdEnuItem == (int)item && x.bActivo.Value
                                    select x).ToList());
            }
            return listaRes;
        }


    }
}
