using MIDIS.SGPVL.ManagerDto.Maestro.Get;

namespace MIDIS.SGPVL.ManagerDto.ComiteAdmin.Get
{
    public class GetAdministrativoFiltersDto
    {
        public List<GetDptoDto> dptos { get; set; }
        public List<GetProvinciaDto> provincias { get; set; }
        public List<GetUbigeoDto> ubigeos{ get; set; }

    }
}
