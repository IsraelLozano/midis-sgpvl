
using MIDIS.SGPVL.Contexto.Data;

namespace MIDIS.SGPVL.Contexto.Seed.Maestros
{
    public class InitMaestro
    {
        private readonly BDPVLContext _context;

        public InitMaestro(BDPVLContext context)
        {
            this._context = context;
        }

        public async Task Create()
        {
            //await new DefaultEnumerado(_context).Create();
            await new DefaultEnumeradoItem(_context).Create();
            await _context.SaveChangesAsync();
        }
    }
}
