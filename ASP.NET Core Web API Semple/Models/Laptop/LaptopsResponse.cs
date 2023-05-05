namespace ASP.NET_Core_Web_API_Semple.Models.Laptop
{
    public class LaptopsResponse : BaseModel
    {
        public List<LaptopSpecification>? Laptops { get; set; }
    }
}
