namespace MIDIS.SGPVL.ManagerDto.Maestro.Get
{
    public class GetDistritoDto
    {
        public string vCodigoDepartamento { get; set; }
        public string vCodigoProvincia { get; set; }
        public string vCodigoDistrito { get; set; }
        public string vDescripcion { get; set; }
        public decimal? dLatitud { get; set; }
        public decimal? dLongitud { get; set; }
        public bool bEstado { get; set; }
        public bool? DISTRITO_PRIORIZADO { get; set; }
        public bool? bVRAEM { get; set; }
        public int? iCodAmbito { get; set; }
    }
}
