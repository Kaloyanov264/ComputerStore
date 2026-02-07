using ComputerStore.DL.Interfaces;
using ComputerStore.Models.Configurations;
using ComputerStore.Models.Dto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ComputerStore.DL.Repositories
{
    internal class ComputerMongoRepository : IComputerRepository
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbConfiguration;
        private readonly ILogger<ComputerMongoRepository> _logger;
        private readonly IMongoCollection<Computer> _computersCollection;

        public ComputerMongoRepository(
            IOptionsMonitor<MongoDbConfiguration> mongoDbConfiguration,
            ILogger<ComputerMongoRepository> logger)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
            _logger = logger;

            var client = new MongoClient(_mongoDbConfiguration.CurrentValue.ConnectionString);

            var database = client.GetDatabase(_mongoDbConfiguration.CurrentValue.DatabaseName);

            _computersCollection = database.GetCollection<Computer>($"{nameof(Computer)}s");
        }

        public void AddComputer(Computer computer)
        {
            if (computer == null) return;

            try
            {
                _computersCollection.InsertOne(computer);
            }
            catch (Exception e)
            {
                _logger.LogError("Error adding computer to DB:{0}-{1}", e.Message, e.StackTrace);
            }
        }

        public void DeleteComputer(Guid? id)
        {
            if (id == null || id == Guid.Empty) return;

            try
            {
                var result = _computersCollection.DeleteOne(c => c.Id == id);

                if (result.DeletedCount == 0)
                {
                    _logger.LogWarning($"No computer found with ID: {id} to delete.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(DeleteComputer)}:{e.Message}-{e.StackTrace}");
            }
        }

        public List<Computer> GetAllComputers()
        {
            return _computersCollection.Find(_ => true).ToList();
        }

        public Computer? GetById(Guid? id)
        {
            if (id == null || id == Guid.Empty) return default;

            try
            {
                return _computersCollection.Find(c => c.Id == id)
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(GetById)}:{e.Message}-{e.StackTrace}");
            }

            return default;
        }

        public void UpdateComputer(Computer computer)
        {
            if (computer == null || computer.Id == Guid.Empty) return;

            try
            {
                var result = _computersCollection.ReplaceOne(
                    c => c.Id == computer.Id,
                    computer
                );

                if (result.MatchedCount == 0)
                {
                    _logger.LogWarning($"No computer found with ID: {computer.Id} to update.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(UpdateComputer)}: {e.Message} - {e.StackTrace}");
            }
        }
    }
}
