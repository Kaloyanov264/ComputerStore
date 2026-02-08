using ComputerStore.BL.Interfaces;
using ComputerStore.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellComputerController : ControllerBase
    {
        private readonly ISellComputer _sellComputer;

        public SellComputerController(ISellComputer sellComputer)
        {
            _sellComputer = sellComputer;
        }

        [HttpPost("Sell")]
        public IActionResult SellComputer([FromBody] SellComputerRequest request)
        {
            try
            {
                var result = _sellComputer.Sell(request.ComputerId, request.CustomerId);
                return Ok(result);
            }

            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
