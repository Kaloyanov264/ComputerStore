using ComputerStore.BL.Interfaces;
using ComputerStore.DL.Interfaces;
using ComputerStore.Models.Responses;

namespace ComputerStore.BL.Services
{
    internal class SellComputer : ISellComputer
    {
        private readonly IComputerCrudService _computerCrudService;
        private readonly ICustomerRepository _customerRepository;

        public SellComputer(IComputerCrudService computerCrudService, ICustomerRepository customerRepository)
        {
            _computerCrudService = computerCrudService;
            _customerRepository = customerRepository;
        }

        public SellComputerResult Sell(Guid computerId, Guid customerId)
        {
            var computer = _computerCrudService.GetById(computerId);

            var customer = _customerRepository.GetById(customerId);

            if (computer == null || customer == null)
            {
                throw new ArgumentException($"Computer with ID {computerId} not found. ");
            }

            var price = computer.BasePrice - customer.Discount; 

            return new SellComputerResult
            {
                Price = price,
                Computer = computer,
                Customer = customer
            };
        }
    }
}
