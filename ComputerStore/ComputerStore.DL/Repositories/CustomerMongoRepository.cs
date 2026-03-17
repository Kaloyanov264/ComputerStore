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

        public async Task AddCustomer(Customer customer)
        {
            if (customer == null) return;

            try
            {
                await _customerCollection.InsertOneAsync(customer);
            }
            catch (Exception e)
            {
                _logger.LogError("Error adding customer to DB:{0}-{1}", e.Message, e.StackTrace);
            }
        }

        public async Task DeleteCustomer(Guid? id)
        {
            if (id == null || id == Guid.Empty) return;

            try
            {
                var result = await _customerCollection.DeleteOneAsync(c => c.Id == id);

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

        public async Task<Customer?> GetById(Guid? id)
        {
            if (id == null || id == Guid.Empty) return default;

            try
            {
                return await _customerCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(GetById)}:{e.Message}-{e.StackTrace}");
            }

            return default;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            try
            {
                return await _customerCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(GetAllCustomers)}:{e.Message}-{e.StackTrace}");
                return new List<Customer>();
            }
        }

        public async Task UpdateCustomer(Customer customer)
        {
            if (customer == null || customer.Id == Guid.Empty) return;

            try
            {
                var result = await _customerCollection.ReplaceOneAsync(
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

