using ComputerStore.BL.Interfaces;
using ComputerStore.DL.Interfaces;
using ComputerStore.Models.Dto;

namespace ComputerStore.BL.Services
{
    internal class CustomerCrudService : ICustomerCrudService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerCrudService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task AddCustomer(Customer customer)
        {
            await _customerRepository.AddCustomer(customer);
        }

        public async Task DeleteCustomer(Guid id)
        {
            await _customerRepository.DeleteCustomer(id);
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _customerRepository.GetAllCustomers();
        }

        public async Task<Customer?> GetById(Guid id)
        {
            return await _customerRepository.GetById(id);
        }

        public async Task UpdateCustomer(Customer customer)
        {
            await _customerRepository.UpdateCustomer(customer);
        }
    }
}
