namespace ComputerStore.Models.Responses
{
    public class UpdateComputerResult
    {
        public Guid Id { get; set; }

        public string Brand { get; set; } = string.Empty;

        public string Cpu { get; set; } = string.Empty;

        public string Ram { get; set; } = string.Empty;

        public string Storage { get; set; } = string.Empty;

        public string Gpu { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public decimal BasePrice { get; set; }
    }
}
