using SQLite;

namespace ASP.NET_Core_Web_API_Semple.Models.Laptop.Specification
{
    public class DisplayModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public double Diagonal { get; set; }
        public string? Resolution { get; set; }
        public DisplayType Type { get; set; }
        public string? UpdateFrequency { get; set; }
    }
}
