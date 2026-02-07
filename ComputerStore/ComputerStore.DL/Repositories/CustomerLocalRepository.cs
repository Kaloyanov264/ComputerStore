using ComputerStore.DL.Interfaces;
using ComputerStore.DL.LocalDb;
using ComputerStore.Models.Dto;

namespace ComputerStore.DL.Repositories
{
    internal class CustomerLocalRepository : ICustomerRepository
    {
        public void AddCustomer(Customer customer)
        {
            StaticDb.Customers.Add(customer);
        }

        public void DeleteCustomer(Guid? id)
        {
            StaticDb.Customers.RemoveAll(c => c.Id == id);
        }

        public Customer? GetById(Guid? id)
        {
            return StaticDb.Customers
                .FirstOrDefault(c =>
                c.Id == id);
        }

        public List<Customer> GetAllCustomers()
        {
            return StaticDb.Customers;
        }

        public void UpdateCustomer(Customer customer)
        {
            var existingCustomer = GetById(customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Email = customer.Email;
            }
        }
    }
}
