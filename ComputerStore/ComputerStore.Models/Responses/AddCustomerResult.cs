namespace ComputerStore.Models.Responses
{
    public class AddCustomerResult
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int Discount { get; set; }
    }
}
