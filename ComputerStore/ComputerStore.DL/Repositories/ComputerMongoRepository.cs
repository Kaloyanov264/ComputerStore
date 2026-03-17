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

        public async Task AddComputer(Computer computer)
        {
            if (computer == null) return;

            try
            {
                await _computersCollection.InsertOneAsync(computer);
            }
            catch (Exception e)
            {
                _logger.LogError("Error adding computer to DB:{0}-{1}", e.Message, e.StackTrace);
            }
        }

        public async Task DeleteComputer(Guid? id)
        {
            if (id == null || id == Guid.Empty) return;

            try
            {
                var result = await _computersCollection.DeleteOneAsync(c => c.Id == id);

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

        public async Task<List<Computer>> GetAllComputers()
        {
            try
            {
                return await _computersCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(GetAllComputers)}:{e.Message}-{e.StackTrace}");
                return new List<Computer>();
            }
        }

        public async Task<Computer?> GetById(Guid? id)
        {
            if (id == null || id == Guid.Empty) return default;

            try
            {
                return await _computersCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method {nameof(GetById)}:{e.Message}-{e.StackTrace}");
            }

            return default;
        }

        public async Task UpdateComputer(Computer computer)
        {
            if (computer == null || computer.Id == Guid.Empty) return;

            try
            {
                var result = await _computersCollection.ReplaceOneAsync(
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
