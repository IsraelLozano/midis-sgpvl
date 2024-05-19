using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Entity.Models.Maestro;
using MIDIS.SGPVL.Utils.Enumerados;

namespace MIDIS.SGPVL.Contexto.Seed.Maestros
{
    public class DefaultEnumeradoItem
    {
        private readonly BDPVLContext _context;

        public DefaultEnumeradoItem(BDPVLContext context)
        {
            this._context = context;
        }

        public async Task Create()
        {
            await CreateVLEnumItem();
        }

        private async Task CreateVLEnumItem()
        {
            var listaItem = new List<VLEnumItem>
            {
                //TIPO_DOCUMENTO = 1
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_DOCUMENTO, vDescripcion = "DNI", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_DOCUMENTO, vDescripcion = "LIB. MILITAR", bActivo = false },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_DOCUMENTO, vDescripcion = "BREVETE", bActivo = false },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_DOCUMENTO, vDescripcion = "R.U.C", bActivo = false },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_DOCUMENTO, vDescripcion = "CARNET FF.PP", bActivo = false },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_DOCUMENTO, vDescripcion = "CARNET FF.AA", bActivo = false },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_DOCUMENTO, vDescripcion = "PASAPORTE", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_DOCUMENTO, vDescripcion = "CARNET IPSS", bActivo = false },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_DOCUMENTO, vDescripcion = "PARTIDA NACIMIENTO", bActivo = false },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_DOCUMENTO, vDescripcion = "OTROS", bActivo = false },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_DOCUMENTO, vDescripcion = "C.E.",  bActivo = true },


                //TIPO_VIA = 2
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_VIA, vDescripcion = "AVENIDA", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_VIA, vDescripcion = "JIRON", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_VIA, vDescripcion = "CALLE", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_VIA, vDescripcion = "PASAJE", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_VIA, vDescripcion = "ALAMEDA", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_VIA, vDescripcion = "MALECON", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_VIA, vDescripcion = "OVALO", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_VIA, vDescripcion = "PLAZA", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_VIA, vDescripcion = "CARRETERA", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_VIA, vDescripcion = "BLOCK", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_VIA, vDescripcion = "SIN ASIGNAR", bActivo = true },


                //TIPO_ZONA = 3
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_ZONA, vDescripcion = "URB. URBANIZACIÓN", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_ZONA, vDescripcion = "P.J. PUEBLO JOVEN", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_ZONA, vDescripcion = "U.V. UNIDAD VECINAL",  bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_ZONA, vDescripcion = "C.H. CONJUNTO HABITACIONAL",bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_ZONA, vDescripcion = "A.H. ASENTAMIENTO HUMANO",bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_ZONA, vDescripcion = "COO. COOPERATIVA", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_ZONA, vDescripcion = "RES. RESIDENCIAL", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_ZONA, vDescripcion = "Z.I. ZONA INDUSTRIAL", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_ZONA, vDescripcion = "GRU. GRUPO", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_ZONA, vDescripcion = "CAS. CASERÍO", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_ZONA, vDescripcion = "FND. FUNDO", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_ZONA, vDescripcion = "OTRO", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_ZONA, vDescripcion = "SIN ASIGNAR", bActivo = true },


                //ESTADO_CIVIL = 4
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.ESTADO_CIVIL, vDescripcion = "SOLTERO", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.ESTADO_CIVIL, vDescripcion = "CASADO", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.ESTADO_CIVIL, vDescripcion = "VIUDO", bActivo = true },

                //MEDIO_CONTACTO = 5,
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.MEDIO_CONTACTO, vDescripcion = "CORREO", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.MEDIO_CONTACTO, vDescripcion = "TELEFONO", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.MEDIO_CONTACTO, vDescripcion = "CELULAR", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.MEDIO_CONTACTO, vDescripcion = "FAX", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.MEDIO_CONTACTO, vDescripcion = "PAGINA WEB", bActivo = true },

                //TIPO_PERSONA = 6,
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_PERSONA, vDescripcion = "NATURAL", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_PERSONA, vDescripcion = "JURIDICA", bActivo = true },

                //GENERO = 7,

                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.GENERO, vDescripcion = "MASCULINO",  bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.GENERO, vDescripcion = "FEMENINO",  bActivo = true },


                //TIPO MONEDA= 8
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_MONEDA, vDescripcion = "DOLARES", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_MONEDA, vDescripcion = "SOLES", bActivo = true },

                //SITUACION - GENERAL = 9
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.SITUACION_GENERAL, vDescripcion = "ABIERTO", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.SITUACION_GENERAL, vDescripcion = "PENDIENTE", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.SITUACION_GENERAL, vDescripcion = "CERRADO", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.SITUACION_GENERAL, vDescripcion = "ACTIVO", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.SITUACION_GENERAL, vDescripcion = "APROBADO", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.SITUACION_GENERAL, vDescripcion = "EN PROCESO", bActivo = true },


                //TIPO_RESOLUCION = 10,
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_RESOLUCION, vDescripcion = "RESOLUCION DE ALCALDIA", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_RESOLUCION, vDescripcion = "RESOLUCION DE GERENCIA", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_RESOLUCION, vDescripcion = "RESOLUCION DE SUBGERENCIA", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_RESOLUCION, vDescripcion = "ACUERDO DE CONSEJO", bActivo = true },
                //TIPO_CARGOS = 11,
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_CARGOS, vDescripcion = "ALCALDE", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_CARGOS, vDescripcion = "FUNCIONARIO MUNICIPAL", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_CARGOS, vDescripcion = "NUTRICIONISTA (MINSA)", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_CARGOS, vDescripcion = "REPRESENTANTE ASOC. PRODUCTORES AGRARIO", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_CARGOS, vDescripcion = "REPRESENTANTE OSB 1", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_CARGOS, vDescripcion = "REPRESENTANTE OSB 2", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_CARGOS, vDescripcion = "REPRESENTANTE OSB 3", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_CARGOS, vDescripcion = "OTROS CARGOS", bActivo = true },
                //TIPO_OSB = 12,
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_OSB, vDescripcion = "CLUB DE MADRES", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_OSB, vDescripcion = "COMITE DE BASO DE LECHE", bActivo = true },
                //TIPO_ALIMENTO = 13,
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_ALIMENTO, vDescripcion = "ALIMENTO 1", bActivo = true },
                //TIPO_SOCIO = 14,
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_SOCIO, vDescripcion = "SOCIO", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_SOCIO, vDescripcion = "SOCIO/USUARIO", bActivo = true },
                //TIPO_CLASIFICACION = 15,
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_CLASIFICACION, vDescripcion = "PRIORIDAD 1", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_CLASIFICACION, vDescripcion = "PRIORIDAD 2", bActivo = true },
                //TIPO_CLASIFICACION_SOCIO_ECONOMICA = 16
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_CLASIFICACION_SOCIO_ECONOMICA, vDescripcion = "POBRE", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_CLASIFICACION_SOCIO_ECONOMICA, vDescripcion = "POBRE EXTREMO", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_CLASIFICACION_SOCIO_ECONOMICA, vDescripcion = "NO POBRE", bActivo = true },
                new VLEnumItem { iIdEnumerado = (int)EnumeradoCabecera.TIPO_CLASIFICACION_SOCIO_ECONOMICA, vDescripcion = "NO CLASIFICADO", bActivo = true },
            };
            await CreateVLEnumItemIfNotExists(listaItem);
        }

        private async Task CreateVLEnumItemIfNotExists(List<VLEnumItem> listaItem)
        {
            var itemsPendientes = listaItem.Where(items => !_context.VLEnumItems.Any(x => x.iIdEnumerado == items.iIdEnumerado && x.vDescripcion.Equals(items.vDescripcion)));
            if (!itemsPendientes.Any())
            {
                return;
            }

            _context.VLEnumItems.AddRange(itemsPendientes);
            await _context.SaveChangesAsync();
        }
    }
}
