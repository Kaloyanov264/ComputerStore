using ComputerStore.Models.Dto;

namespace ComputerStore.DL.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddCustomer(Customer customer);

        Task DeleteCustomer(Guid? id);

        Task<List<Customer>> GetAllCustomers();

        Task<Customer?> GetById(Guid? id);

        void UpdateCustomer(Customer customer);
    }
}
