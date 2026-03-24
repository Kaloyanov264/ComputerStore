using ComputerStore.Models.Dto;

namespace ComputerStore.BL.Interfaces
{
    public interface ICustomerCrudService
    {
        Task AddCustomer(Customer customer);

        Task DeleteCustomer(Guid id);

        Task UpdateCustomer(Customer customer);

        Task<List<Customer>> GetAllCustomers();

        Task<Customer?> GetById(Guid id);
    }
}
