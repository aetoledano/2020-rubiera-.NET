using Microsoft.AspNetCore.Mvc;

namespace rubiera.Controllers
{
    [Route("weather")]
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}