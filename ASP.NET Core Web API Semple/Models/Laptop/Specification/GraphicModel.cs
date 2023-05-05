using SQLite;

namespace ASP.NET_Core_Web_API_Semple.Models.Laptop.Specification
{
    public class GraphicModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string? Name { get; set; }
        public int Memory { get; set; }
        public GraphicType Type { get; set; }
    }
}
