using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_API_Semple.Controllers
{
    public class BaseController : Controller
    {
        protected static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        protected static double RandomDoubleNumber(double min, double max)
        {
            Random random = new Random();
            return random.NextDouble() * (max - min) + min;
        }

        protected static int EvenRandomNumber(int min, int max)
        {
            Random random = new Random();
            int number = random.Next(min, max);

            return number % 2 != 0 ? number - 1 : number;
        }
    }
}
