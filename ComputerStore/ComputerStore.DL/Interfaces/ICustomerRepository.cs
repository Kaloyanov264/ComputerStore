using ComputerStore.Models.Dto;

namespace ComputerStore.DL.Interfaces
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);

        void DeleteCustomer(Guid? id);

        List<Customer> GetAllCustomers();

        Customer? GetById(Guid? id);

        void UpdateCustomer(Customer customer);
    }
}
