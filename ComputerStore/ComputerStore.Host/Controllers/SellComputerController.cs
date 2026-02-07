using ComputerStore.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellComputerController : Controller
    {
        [HttpPost("Sell")]
        public IActionResult SellComputer([FromBody] SellComputerRequest request)
        {
            return Ok();
        }
    }
}
