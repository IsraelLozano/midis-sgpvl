
using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Entity.Models.Maestro;

namespace MIDIS.SGPVL.Contexto.Seed.Maestros
{
    public class DefaultEnumerado
    {
        private readonly BDPVLContext _context;

        public DefaultEnumerado(BDPVLContext contexto)
        {
            this._context = contexto;
        }

        public async Task Create()
        {
            await CreateVLEnumerado();
        }

        private async Task CreateVLEnumerado()
        {
            var enumerado = new List<VLEnumerado> {
                new VLEnumerado { vDescripcion = "TIPO DOCUMENTO", bActivo = true },
                new VLEnumerado { vDescripcion = "TIPOS DE VIA", bActivo = true },
                new VLEnumerado { vDescripcion = "TIPOS DE ZONA", bActivo = true },
                new VLEnumerado { vDescripcion = "ESTADO CIVIL", bActivo = true },
                new VLEnumerado { vDescripcion = "MEDIO CONTACTO", bActivo = true },
                new VLEnumerado { vDescripcion = "TIPO PERSONA", bActivo = true },
                new VLEnumerado { vDescripcion = "GENERO", bActivo = true },
                new VLEnumerado { vDescripcion = "TIPO MONEDA", bActivo = true },
                new VLEnumerado { vDescripcion = "SITUACION", bActivo = true },
                new VLEnumerado { vDescripcion = "TIPO RESOLUCION", bActivo = true },
                new VLEnumerado { vDescripcion = "TIPO CARGOS", bActivo = true },
                new VLEnumerado { vDescripcion = "TIPO OSB", bActivo = true },
                new VLEnumerado { vDescripcion = "TIPO ALIMENTO", bActivo = true },
                new VLEnumerado { vDescripcion = "TIPO SOCIO", bActivo = true },
                new VLEnumerado { vDescripcion = "TIPO CLASIFICACION", bActivo = true },
                new VLEnumerado { vDescripcion = "TIPO CLASIFICACION SOCIO ECONOMICA", bActivo = true },
                new VLEnumerado { vDescripcion = "TIPO CARGO JD", bActivo = true },
            };
            await CreateMaestraIfNotExists(enumerado);
        }

        private async Task CreateMaestraIfNotExists(List<VLEnumerado> enumerados)
        {
            var defaultParameter = enumerados
                    .Where(item => !_context.VLEnumerados
                                    .Any(x => x.vDescripcion.Equals(item.vDescripcion)))
                    .ToList();
            if (!defaultParameter.Any())
            {
                return;
            }

            _context.VLEnumerados.AddRange(defaultParameter);
            await _context.SaveChangesAsync();
        }
    }
}
