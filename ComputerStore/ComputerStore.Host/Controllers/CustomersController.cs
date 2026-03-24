using ComputerStore.BL.Interfaces;
using ComputerStore.Models.Dto;
using ComputerStore.Models.Requests;
using ComputerStore.Models.Responses;
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
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerCrudService.GetAllCustomers();
            return Ok(customers);
        }

        [HttpPost(nameof(AddCustomer))]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerRequest? customerRequest)
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

            await _customerCrudService.AddCustomer(customer);

            return Ok(new AddCustomerResult
            {
                Id = customer.Id
            });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer([FromQuery] DeleteCustomerRequest customerRequest)
        {
            var customer = _customerCrudService.GetById(customerRequest.Id);
            if (customer == null)
            {
                return NotFound($"Customer with ID {customerRequest.Id} not found.");
            }

            await _customerCrudService.DeleteCustomer(customerRequest.Id);

            return Ok(new DeleteCustomerResult());
        }

        [HttpGet(nameof(GetById))]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _customerCrudService.GetById(id);

            if (customer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }
            return Ok(customer);
        }

        [HttpPost(nameof(UpdateCustomer))]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerRequest customerRequest)
        {
            if (customerRequest == null)
            {
                return BadRequest("Customer data is null.");
            }
            var existingCustomer = _customerCrudService.GetById(customerRequest.Id);
            if (existingCustomer == null)
            {
                return NotFound($"Customer with ID {customerRequest.Id} not found.");
            }

            var customer = _mapper.Map<Customer>(customerRequest);
            await _customerCrudService.UpdateCustomer(customer);
            return Ok(new UpdateCustomerResult
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Discount = customer.Discount
            });
        }
    }
}
