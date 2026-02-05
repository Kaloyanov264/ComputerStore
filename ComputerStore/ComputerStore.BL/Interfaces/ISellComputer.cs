using ComputerStore.Models.Responses;

namespace ComputerStore.BL.Interfaces
{
    public interface ISellComputer
    {
        SellComputerResult Sell(Guid computerId, Guid customerId);
    }
}
