using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Core_Web_API_Semple.Models;
using ASP.NET_Core_Web_API_Semple.Database;
using ASP.NET_Core_Web_API_Semple.Models.Laptop;
using ASP.NET_Core_Web_API_Semple.Models.Laptop.Specification;

namespace ASP.NET_Core_Web_API_Semple.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LaptopController : BaseController
    {
        private readonly ILogger<LaptopController> logger;
        public LaptopController(ILogger<LaptopController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public string Create()
        {
            try
            {
                ApiContext context = new ApiContext();
                LaptopModel laptop = new LaptopModel
                {
                    ErrorCode = ErrorCode.OK,
                    Laptop = new LaptopSpecification
                    {
                        Name = "LaptopName",
                        Manufacturer = (Manufacturer)RandomNumber(0, 2),
                        CPU = new CPUModel
                        {
                            Name = (CPU_Name)RandomNumber(0, 1),
                            CoresQuantity = EvenRandomNumber(4, 10),
                            Frequency = $"{Math.Round(RandomDoubleNumber(2.1, 3.6), 1)} GHz"
                        },
                        Memory = new MemoryModel
                        {
                            RAM = $"16 RAM",
                            RAMType = (RAM_Type)RandomNumber(0, 3),
                            Storage = RandomNumber(0, 1) % 2 != 0 ? 150 : 500,
                            StorageType = (StorageType)RandomNumber(0, 1),
                        },
                        Display = new DisplayModel
                        {
                            Diagonal = Math.Round(RandomDoubleNumber(15.6, 19.5), 1),
                            Resolution = "1920 x 1080",
                            Type = (DisplayType)RandomNumber(0, 2),
                            UpdateFrequency = "120Hz"
                        },
                        Graphic = new GraphicModel
                        {
                            Name = "Graphic-Card name",
                            Memory = EvenRandomNumber(4, 8),
                            Type = (GraphicType)RandomNumber(0, 1)
                        }
                    }
                };

                context.Add(laptop.Laptop);
                context.SaveChanges();

                string json = JsonSerializer.Serialize(laptop);
                logger.LogInformation("Laptop is successfully created");

                return json;
            }
            catch (Exception ex)
            {
                logger.LogError($"Create is failed => ErrorMessage: {ex}");
                return JsonSerializer.Serialize(new LaptopModel
                {
                    ErrorCode = ErrorCode.IternalError,
                    ErrorMessage = ex.ToString()
                });
            }
        }

        [HttpPost]
        public string Update(LaptopSpecification update)
        {
            try
            {
                ApiContext context = new ApiContext();
                LaptopSpecification? laptop = context.Laptops.FirstOrDefault(laptop => laptop.ID == update.ID);

                if (laptop != null)
                {
                    laptop.Name = update.Name;
                    laptop.Manufacturer = update.Manufacturer;
                    laptop.CPU = update.CPU;
                    laptop.Memory = update.Memory;
                    laptop.Display = update.Display;
                    laptop.Graphic = update.Graphic;

                    context.SaveChanges();

                    logger.LogError("Update is successful");
                    return JsonSerializer.Serialize(new BaseModel
                    {
                        ErrorCode = ErrorCode.OK
                    });
                }

                logger.LogError("Update is failed => object not exist");
                return JsonSerializer.Serialize(new BaseModel
                {
                    ErrorCode = ErrorCode.NotExist
                });
            }
            catch(Exception ex)
            {
                logger.LogError("Update is failed");
                return JsonSerializer.Serialize(new BaseModel
                {
                    ErrorCode = ErrorCode.IternalError,
                    ErrorMessage = ex.ToString()
                });
            }
        }

        [HttpGet]
        public string Remove(int id)
        {
            try
            {
                ApiContext context = new ApiContext();
                LaptopSpecification? laptop = context.Laptops.FirstOrDefault(laptop => laptop.ID == id);

                if (laptop != null)
                {
                    context.Remove(laptop);
                    context.SaveChanges();

                    logger.LogInformation("Remove is successful");
                    return JsonSerializer.Serialize(new BaseModel
                    {
                        ErrorCode = ErrorCode.OK
                    });
                }

                logger.LogError("Remove is failed => object not exist");
                return JsonSerializer.Serialize(new BaseModel
                {
                    ErrorCode = ErrorCode.NotExist
                });
            }
            catch(Exception ex)
            {
                logger.LogError($"Remove is failed => ErrorMessage: {ex}");
                return JsonSerializer.Serialize(new BaseModel
                {
                    ErrorCode = ErrorCode.IternalError,
                    ErrorMessage = ex.ToString()
                });
            }
        }

        [HttpGet]
        public string GetLaptop(int id)
        {
            try
            {
                ApiContext context = new ApiContext();
                LaptopSpecification? laptop = context.Laptops.Include(c => c.CPU)
                    .Include(m => m.Memory)
                    .Include(d => d.Display)
                    .Include(g => g.Graphic)
                    .FirstOrDefault(laptop => laptop.ID == id);

                if (laptop != null)
                {
                    LaptopResponse response = new LaptopResponse
                    {
                        ErrorCode = ErrorCode.OK,
                        Laptop = laptop
                    };

                    string json = JsonSerializer.Serialize(response);
                    logger.LogInformation("GetLaptop is successful");

                    return json;
                }

                logger.LogInformation("GetLaptop is failed => object not exist");
                return JsonSerializer.Serialize(new LaptopResponse
                {
                    ErrorCode = ErrorCode.NotExist
                });
            }
            catch(Exception ex)
            {
                logger.LogError($"GetLaptop is failed => {ex}");
                return JsonSerializer.Serialize(new LaptopResponse
                {
                    ErrorCode = ErrorCode.IternalError,
                    ErrorMessage = ex.ToString()
                });
            }
        }

        [HttpGet]
        public string GetLaptops()
        {
            try
            {
                ApiContext context = new ApiContext();

                LaptopsResponse response = new LaptopsResponse
                {
                    ErrorCode = ErrorCode.OK,
                    Laptops = context.Laptops.Include(cpu => cpu.CPU)
                        .Include(m => m.Memory)
                        .Include(d => d.Display)
                        .Include(g => g.Graphic).ToList()
                };

                string json = JsonSerializer.Serialize(response);
                logger.LogInformation("GetLaptops is successful");

                return json;
            }
            catch(Exception ex)
            {
                logger.LogError($"GetLaptops is failed => ErrorMessage: {ex}");
                return JsonSerializer.Serialize(new LaptopsResponse
                {
                    ErrorCode = ErrorCode.IternalError,
                    ErrorMessage = ex.ToString()
                });
            }
        }

        [HttpGet]
        public string RemoveAll()
        {
            try
            {
                ApiContext context = new ApiContext();

                context.Database.EnsureDeleted();
                context.SaveChanges();

                logger.LogInformation("RemoveAll is successful");
                return JsonSerializer.Serialize(new BaseModel
                {
                    ErrorCode = ErrorCode.OK
                });
            }
            catch(Exception ex)
            {
                logger.LogError($"RemoveAll is failed => ErrorMessage: {ex}");
                return JsonSerializer.Serialize(new BaseModel
                {
                    ErrorCode = ErrorCode.IternalError,
                    ErrorMessage = ex.ToString()
                });
            }
        }
    }
}
