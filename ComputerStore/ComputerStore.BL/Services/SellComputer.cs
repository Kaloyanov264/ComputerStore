using ComputerStore.BL.Interfaces;
using ComputerStore.DL.Interfaces;
using ComputerStore.Models.Responses;
using Microsoft.Extensions.Logging;

namespace ComputerStore.BL.Services
{
    internal class SellComputer : ISellComputer
    {
        private readonly IComputerCrudService _computerCrudService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IKafkaProducerService _kafkaProducer;
        private readonly ILogger<SellComputer> _logger;


        public SellComputer(IComputerCrudService computerCrudService,
            ICustomerRepository customerRepository,
            IKafkaProducerService kafkaProducer, 
            ILogger<SellComputer> logger)
        {
            _kafkaProducer = kafkaProducer;
            _computerCrudService = computerCrudService;
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<SellComputerResult> Sell(Guid computerId, Guid customerId)
        {
            var computer = await _computerCrudService.GetById(computerId);

            var customer = await _customerRepository.GetById(customerId);

            if (computer == null || customer == null)
            {
                throw new ArgumentException($"Computer with ID {computerId} not found. ");
            }

            var message = $"Sold Computer ID: {computerId} to Customer ID: {customerId}";

            int messageKey = 112111;

            await _kafkaProducer.ProduceSaleMessageAsync(messageKey, message);

            var price = computer.BasePrice - customer.Discount;

            _logger.LogInformation("[KAFKA] {Message}", message);

            return new SellComputerResult
            {
                Price = price,
                Computer = computer,
                Customer = customer
            };
        }
    }
}
