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

        public void AddCustomer(Customer customer)
        {
            _customerRepository.AddCustomer(customer);
        }

        public void DeleteCustomer(Guid id)
        {
            _customerRepository.DeleteCustomer(id);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAllCustomers();
        }

        public Customer? GetById(Guid id)
        {
            return _customerRepository.GetById(id);
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.UpdateCustomer(customer);
        }
    }
}
