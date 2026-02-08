using ComputerStore.Models.Dto;

namespace ComputerStore.Models.Requests
{
    public class SellComputerRequest
    {
        public Guid ComputerId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
