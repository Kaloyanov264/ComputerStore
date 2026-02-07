namespace ComputerStore.Models.Requests
{
    public class AddComputerRequest
    {
        public string Brand { get; set; } = string.Empty;

        public string Cpu { get; set; } = string.Empty;

        public string Ram { get; set; } = string.Empty;

        public string Storage { get; set; } = string.Empty;

        public string Gpu { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public decimal BasePrice { get; set; }
    }
}
