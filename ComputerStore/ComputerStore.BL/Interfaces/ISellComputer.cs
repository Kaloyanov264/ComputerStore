using ComputerStore.Models.Responses;

namespace ComputerStore.BL.Interfaces
{
    public interface ISellComputer
    {
        Task<SellComputerResult> Sell(Guid computerId, Guid customerId);
    }
}
