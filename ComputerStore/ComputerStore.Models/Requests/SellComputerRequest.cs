using ComputerStore.Models.Dto;

namespace ComputerStore.Models.Requests
{
    public class SellComputerRequest
    {
        public Computer Computer { get; set; }

        public Customer Customer { get; set; }

        public decimal Price { get; set; }
    }
}
