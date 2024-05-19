using MIDIS.SGPVL.ManagerDto.Base;
using MIDIS.SGPVL.ManagerDto.Maestro.Get;

namespace MIDIS.SGPVL.ManagerDto.ComitePvl.Get
{
    public class GetComiteParams
    {
        public Search search { get; set; }
        public List<Order> order { get; set; }
        public List<Column> columns { get; set; }
        public int length { get; set; }
        public int start { get; set; }
        public int draw { get; set; }

        //Adicionales
        public string? idDptoSelect { get; set; }
        public string? idProvSelect { get; set; }
        public string? idUbigeo { get; set; }
        public string? idCentroPoblado { get; set; }
        public List<GetDptoDto> dptos { get; set; }

    }
}
