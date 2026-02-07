using ComputerStore.Models.Dto;

namespace ComputerStore.Tests.MockData
{
    internal static class CustomerMockedData
    {
        public static List<Customer> Customers =
            new List<Customer>()
            {
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Ivan Dimitrov",
                    Email = "IvD@xxx.com"
                },

                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Boris Kaloyanov",
                    Email = "BorisK@gmail.com"
                },

                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Gergana Borislavova",
                    Email = "GerBor@gmail.com"
                }
            };
    }
}
