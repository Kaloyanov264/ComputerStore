using ComputerStore.BL.Services;
using ComputerStore.DL.Interfaces;
using ComputerStore.Models.Dto;
using ComputerStore.Tests.MockData;
using Moq;

namespace ComputerStore.Tests.CustomerTests
{
    public class CustomerCrudServiceTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;

        public CustomerCrudServiceTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
        }

        [Fact]
        public void AddCustomerTest_Ok()
        {
            //setup
            var expectedCustomerCount = CustomerMockedData.Customers.Count + 1;
            var id = Guid.NewGuid();
            var customer = new Customer
            {
                Id = id,
                Name = "John Doe",
                Email = "jd@xxx.com"
            };

            Customer resultCustomer = null;
            _customerRepositoryMock
                .Setup(repo => repo.AddCustomer(customer))
                .Callback(() =>
                {
                    CustomerMockedData.Customers.Add(customer);
                });

            //inject
            var service = new CustomerCrudService(_customerRepositoryMock.Object);

            //act
            service.AddCustomer(customer);

            resultCustomer = CustomerMockedData.Customers.FirstOrDefault(c => c.Id == id);
            //assert
            Assert.NotNull(resultCustomer);
            Assert.Contains(customer, CustomerMockedData.Customers);
            Assert.Equal(expectedCustomerCount, CustomerMockedData.Customers.Count);
            Assert.Equal(id, resultCustomer.Id);
        }

        [Fact]
        public void DeleteCustomerTest_Ok()
        {
            // setup
            var customerToDelete = CustomerMockedData.Customers.First();
            var expectedCustomerCount = CustomerMockedData.Customers.Count - 1;

            _customerRepositoryMock
                .Setup(repo => repo.DeleteCustomer(customerToDelete.Id))
                .Callback(() =>
                {
                    var customer = CustomerMockedData.Customers
                        .FirstOrDefault(c => c.Id == customerToDelete.Id);

                    if (customer != null)
                    {
                        CustomerMockedData.Customers.Remove(customer);
                    }
                });

            // inject
            var service = new CustomerCrudService(_customerRepositoryMock.Object);

            // act
            service.DeleteCustomer(customerToDelete.Id);

            var deletedCustomer = CustomerMockedData.Customers
                .FirstOrDefault(c => c.Id == customerToDelete.Id);

            // assert
            Assert.Null(deletedCustomer);
            Assert.Equal(expectedCustomerCount, CustomerMockedData.Customers.Count);
        }

        [Fact]
        public void UpdateCustomerTest_Ok()
        {
            // setup
            var existingCustomer = CustomerMockedData.Customers.First();
            var updatedName = "Jane Doe";
            var updatedEmail = "jane@xxx.com";
            var expectedCustomerCount = CustomerMockedData.Customers.Count;

            var updatedCustomer = new Customer
            {
                Id = existingCustomer.Id,
                Name = updatedName,
                Email = updatedEmail
            };

            _customerRepositoryMock
                .Setup(repo => repo.UpdateCustomer(updatedCustomer))
                .Callback(() =>
                {
                    var customer = CustomerMockedData.Customers
                        .FirstOrDefault(c => c.Id == updatedCustomer.Id);

                    if (customer != null)
                    {
                        customer.Name = updatedCustomer.Name;
                        customer.Email = updatedCustomer.Email;
                    }
                });

            // inject
            var service = new CustomerCrudService(_customerRepositoryMock.Object);

            // act
            service.UpdateCustomer(updatedCustomer);

            var resultCustomer = CustomerMockedData.Customers
                .FirstOrDefault(c => c.Id == updatedCustomer.Id);

            // assert
            Assert.NotNull(resultCustomer);
            Assert.Equal(updatedName, resultCustomer.Name);
            Assert.Equal(updatedEmail, resultCustomer.Email);
            Assert.Equal(expectedCustomerCount, CustomerMockedData.Customers.Count);
        }
    }
}
