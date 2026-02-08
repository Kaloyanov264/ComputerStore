namespace ComputerStore.Models.Responses
{
    public class UpdateCustomerResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int Discount { get; set; }
    }
}
