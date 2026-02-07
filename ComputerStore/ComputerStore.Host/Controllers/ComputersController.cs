using ComputerStore.BL.Interfaces;
using ComputerStore.Models.Dto;
using ComputerStore.Models.Requests;
using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputersController : ControllerBase
    {
        private readonly IComputerCrudService _computerCrudService;
        private readonly IMapper _mapper;
        private IValidator<AddComputerRequest> _validator;

        public ComputersController(
            IComputerCrudService computerCrudService,
            IMapper mapper,
            IValidator<AddComputerRequest> validator)
        {
            _computerCrudService = computerCrudService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var computers = _computerCrudService.GetAllComputers();
            return Ok(computers);
        }

        [HttpPost(nameof(AddComputer))]
        public IActionResult AddComputer([FromBody] AddComputerRequest? computerRequest)
        {
            if (computerRequest == null)
            {
                return BadRequest("Computer data is null.");
            }

            var result = _validator.Validate(computerRequest);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var computer = _mapper.Map<Computer>(computerRequest);

            _computerCrudService.AddComputer(computer);

            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteComputer(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id must be a valid Guid.");
            }

            var computer = _computerCrudService.GetById(id);
            if (computer == null)
            {
                return NotFound($"Computer with ID {id} not found.");
            }

            _computerCrudService.DeleteComputer(id);

            return Ok();
        }

        [HttpGet(nameof(GetById))]
        public IActionResult GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id must be a valid Guid.");
            }

            var computer = _computerCrudService.GetById(id);

            if (computer == null)
            {
                return NotFound($"Computer with ID {id} not found.");
            }
            return Ok(computer);
        }

        [HttpPost(nameof(UpdateComputer))]
        public IActionResult UpdateComputer([FromBody] Computer? computer)
        {
            if (computer == null)
            {
                return BadRequest("Computer data is null.");
            }
            var existingComputer = _computerCrudService.GetById(computer.Id);
            if (existingComputer == null)
            {
                return NotFound($"Computer with ID {computer.Id} not found.");
            }
            _computerCrudService.UpdateComputer(computer);
            return Ok();
        }
    }
}
