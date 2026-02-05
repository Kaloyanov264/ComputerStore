using ComputerStore.Models.Dto;

namespace ComputerStore.DL.Interfaces
{
    public interface IComputerRepository
    {
        void AddComputer(Computer computer);

        void DeleteComputer(Guid? id);

        void UpdateComputer(Computer computer);

        List<Computer> GetAllComputers();

        Computer? GetById(Guid? id);   
    }
}
