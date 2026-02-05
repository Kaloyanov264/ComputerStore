using ComputerStore.Models.Dto;

namespace ComputerStore.BL.Interfaces
{
    public interface ICustomerCrudService
    {
        void AddCustomer(Customer customer);

        void DeleteCustomer(Guid id);

        void UpdateCustomer(Customer customer);

        List<Customer> GetAllCustomers();

        Customer? GetById(Guid id);
    }
}
