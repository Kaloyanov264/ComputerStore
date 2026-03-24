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

        public async Task AddComputer(Computer computer)
        {
            if (computer == null) return;

            if (computer?.Id == null || computer.Id == Guid.Empty)
            {
                computer!.Id = Guid.NewGuid();
            }

            await _computerRepository.AddComputer(computer);
        }

        public async Task DeleteComputer(Guid id)
        {
            await _computerRepository.DeleteComputer(id);
        }

        public async Task<List<Computer>> GetAllComputers()
        {
            return await _computerRepository.GetAllComputers();
        }

        public async Task<Computer?> GetById(Guid id)
        {
            return await _computerRepository.GetById(id);
        }

        public async Task UpdateComputer(Computer computer)
        {
            await _computerRepository.UpdateComputer(computer);
        }
    }
}
