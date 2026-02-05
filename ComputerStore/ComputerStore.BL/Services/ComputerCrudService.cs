using ComputerStore.BL.Interfaces;
using ComputerStore.DL.Interfaces;
using ComputerStore.Models.Dto;

namespace ComputerStore.BL.Services
{
    internal class ComputerCrudService : IComputerCrudService
    {
        private readonly IComputerRepository _computerRepository;

        public ComputerCrudService(IComputerRepository computerRepository)
        {
            _computerRepository = computerRepository;
        }

        public void AddComputer(Computer computer)
        {
            if (computer == null) return;

            if (computer?.Id == null || computer.Id == Guid.Empty)
            {
                computer!.Id = Guid.NewGuid();
            }

            _computerRepository.AddComputer(computer);
        }

        public void DeleteComputer(Guid id)
        {
            _computerRepository.DeleteComputer(id);
        }

        public List<Computer> GetAllComputers()
        {
            return _computerRepository.GetAllComputers();
        }

        public Computer? GetById(Guid id)
        {
            return _computerRepository.GetById(id);
        }

        public void UpdateComputer(Computer computer)
        {
            _computerRepository.UpdateComputer(computer);
        }
    }
}
