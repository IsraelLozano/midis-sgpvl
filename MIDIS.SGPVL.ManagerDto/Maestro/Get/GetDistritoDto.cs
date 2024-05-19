namespace MIDIS.SGPVL.ManagerDto.Maestro.Get
{
    public class GetDistritoDto
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }

        public string dpto { get; set; }
        public string provincia { get; set; }
        public string full() => $"{dpto} / {provincia} / {descripcion}";
    }
}
