namespace MIDIS.SGPVL.AppWeb.Models
{
    public class dataParams
    {
        public Search search { get; set; }
        public List<Order> order { get; set; }
        public List<Column> columns { get; set; }
        public int length { get; set; }
        public int start { get; set; }
        public int draw { get; set; }
    }
}
