using SQLite;

namespace ASP.NET_Core_Web_API_Semple.Models.Laptop.Specification
{
    public class MemoryModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string? RAM { get; set; }
        public RAM_Type RAMType { get; set; }
        public int Storage { get; set; }
        public StorageType StorageType { get; set; }
    }
}
