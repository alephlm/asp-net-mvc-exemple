using Microsoft.AspNetCore.Mvc;

namespace Invoicing.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
