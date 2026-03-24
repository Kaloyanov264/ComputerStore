using ComputerStore.Models.Dto;

namespace ComputerStore.BL.Interfaces
{
    public interface IComputerCrudService
    {
        Task AddComputer(Computer computer);

        Task DeleteComputer(Guid id);

        Task UpdateComputer(Computer computer);

        Task<List<Computer>> GetAllComputers();

        Task<Computer?> GetById(Guid id);
    }
}
