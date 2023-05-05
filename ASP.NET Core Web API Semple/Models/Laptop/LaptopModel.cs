using SQLite;
using SQLiteNetExtensions.Attributes;
using ASP.NET_Core_Web_API_Semple.Models.Laptop.Specification;


namespace ASP.NET_Core_Web_API_Semple.Models.Laptop
{
    public class LaptopModel : BaseModel
    {
        public LaptopSpecification? Laptop { get; set; }
    }

    public class LaptopSpecification
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string? Name { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public CPUModel? CPU { get; set; }
        public MemoryModel? Memory { get; set; }
        public DisplayModel? Display { get; set; }
        public GraphicModel? Graphic { get; set; }
    }
}
