using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Host.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
