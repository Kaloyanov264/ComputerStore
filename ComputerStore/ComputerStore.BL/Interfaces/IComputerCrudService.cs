using ComputerStore.Models.Dto;

namespace ComputerStore.BL.Interfaces
{
    public interface IComputerCrudService
    {
        void AddComputer(Computer computer);

        void DeleteComputer(Guid id);

        void UpdateComputer(Computer computer);

        List<Computer> GetAllComputers();

        Computer? GetById(Guid id);
    }
}
