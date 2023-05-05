using SQLite;

namespace ASP.NET_Core_Web_API_Semple.Models.Laptop.Specification
{
    public class CPUModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public CPU_Name Name { get; set; }
        public int CoresQuantity { get; set; }
        public string? Frequency { get; set; }
    }
}
