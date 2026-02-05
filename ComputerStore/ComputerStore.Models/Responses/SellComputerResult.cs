using ComputerStore.Models.Dto;

namespace ComputerStore.Models.Responses
{
    public class SellComputerResult
    {
        public Computer Computer { get; set; }

        public Customer Customer { get; set; }

        public decimal Price { get; set; }
    }
}
