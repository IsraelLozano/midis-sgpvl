using MIDIS.SGPVL.Contexto.Data;
using MIDIS.SGPVL.Entity.Models.Maestro;
using PROMART.SISTRAN.Repository.Base;
using System;
using System.Threading.Tasks;

namespace MIDIS.SGPVL.Repository.Maestro
{
    public class DistritoRepository : GenericRepository<MaeDistrito>, IDistritoRepository
    {
        private readonly BDPVLContext _context;

        public DistritoRepository(BDPVLContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<MaeDistrito>> GetDistritoFullByParamAsync(List<string> ubigeos)
        {
            try
            {
                var query = from d in _context.MaeDistritos
                            join p in _context.MaeProvincia on new { d.vCodigoDepartamento, d.vCodigoProvincia } equals new { p.vCodigoDepartamento, p.vCodigoProvincia }
                            join dpto in _context.MaeDepartamentos on d.vCodigoDepartamento equals dpto.vCodigoDepartamento
                            select new MaeDistrito
                            {
                                vDescripcion = d.vDescripcion,
                                vCodigoDistrito = d.vCodigoDistrito,
                                vCodigoProvincia = d.vCodigoProvincia,
                                vCodigoDepartamento = d.vCodigoDepartamento,
                                NombreDistrito = d.vDescripcion,
                                NombreProvincia = p.vDescripcion,
                                NombreDpto = dpto.vDescripcion,
                                codUbigeoFull = d.vCodigoDepartamento + d.vCodigoProvincia + d.vCodigoDistrito

                            };
                var response = query
                    .Where(l => ubigeos.Contains(l.codUbigeoFull))
                    .ToList();
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
