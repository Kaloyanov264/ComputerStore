using ComputerStore.DL.Interfaces;
using ComputerStore.Models.Configurations;
using ComputerStore.Models.Dto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ComputerStore.DL.Repositories
{
    internal class CustomerMongoRepository : ICustomerRepository
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbConfiguration;
        private readonly ILogger<CustomerMongoRepository> _logger;
        private readonly IMongoCollection<Customer> _customerCollection;

        public CustomerMongoRepository(
            IOptionsMonitor<MongoDbConfiguration> mongoDbConfiguration,
            ILogger<CustomerMongoRepository> logger)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
            _logger = logger;

            var client = new MongoClient(_mongoDbConfiguration.CurrentValue.ConnectionString);

            var database = client.GetDatabase(_mongoDbConfiguration.CurrentValue.DatabaseName);

            _customerCollection = database.GetCollection<Customer>($"{nameof(Customer)}s");
        }

        public void AddCustomer(Customer customer)
        {
            if (customer == null) return;

            try
            {
                _customerCollection.InsertOne(customer);
            }
            catch (Exception e)
            {
                _logger.LogError("Error adding customer to DB:{0}-{1}", e.Message, e.StackTrace);
            }
        }

        public void DeleteCustomer(Guid? id)
        {
            if (id == null || id == Guid.Empty) return;

            try
            {
                var result = _customerCollection.DeleteOne(c => c.Id == id);

                if (result.DeletedCount == 0)
                {
                    _logger.LogWarning($"No customer found with ID: {id} to delete.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(DeleteCustomer)}:{e.Message}-{e.StackTrace}");
            }
        }

        public Customer? GetById(Guid? id)
        {
            if (id == null || id == Guid.Empty) return default;

            try
            {
                return _customerCollection.Find(c => c.Id == id)
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(GetById)}:{e.Message}-{e.StackTrace}");
            }

            return default;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerCollection.Find(_ => true).ToList();
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer == null || customer.Id == Guid.Empty) return;

            try
            {
                var result = _customerCollection.ReplaceOne(
                    c => c.Id == customer.Id,
                    customer
                );

                if (result.MatchedCount == 0)
                {
                    _logger.LogWarning($"No customer found with ID: {customer.Id} to update.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(UpdateCustomer)}:{e.Message}-{e.StackTrace}");
            }
        }
    }
}

