using Microsoft.AspNetCore.Mvc;

namespace spotify_api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
