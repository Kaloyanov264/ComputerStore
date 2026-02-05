using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Host.Controllers
{
    public class ComputersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
