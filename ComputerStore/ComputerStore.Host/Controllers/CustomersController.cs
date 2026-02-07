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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerCrudService _customerCrudService;
        private IValidator<AddCustomerRequest> _validator;
        private readonly IMapper _mapper;   

        public CustomersController(ICustomerCrudService customerCrudService, IValidator<AddCustomerRequest> validator, IMapper mapper)
        {
            _customerCrudService = customerCrudService;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var customers = _customerCrudService.GetAllCustomers();
            return Ok(customers);
        }

        [HttpPost(nameof(AddCustomer))]
        public IActionResult AddCustomer([FromBody] AddCustomerRequest? customerRequest)
        {
            if (customerRequest == null)
            {
                return BadRequest("Customer data is null.");
            }

            var result = _validator.Validate(customerRequest);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var customer = _mapper.Map<Customer>(customerRequest);

            _customerCrudService.AddCustomer(customer);

            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteCustomer(Guid id)
        {
            var customer = _customerCrudService.GetById(id);
            if (customer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }

            _customerCrudService.DeleteCustomer(id);

            return Ok();
        }

        [HttpGet(nameof(GetById))]
        public IActionResult GetById(Guid id)
        {
            var customer = _customerCrudService.GetById(id);

            if (customer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }
            return Ok(customer);
        }

        [HttpPost(nameof(UpdateCustomer))]
        public IActionResult UpdateCustomer([FromBody] Customer? customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer data is null.");
            }
            var existingCustomer = _customerCrudService.GetById(customer.Id);
            if (existingCustomer == null)
            {
                return NotFound($"Customer with ID {customer.Id} not found.");
            }
            _customerCrudService.UpdateCustomer(customer);
            return Ok();
        }
    }
}
