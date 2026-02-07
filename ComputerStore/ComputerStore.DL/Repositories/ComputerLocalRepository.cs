using ComputerStore.DL.Interfaces;
using ComputerStore.DL.LocalDb;
using ComputerStore.Models.Dto;

namespace ComputerStore.DL.Repositories
{
    [Obsolete($"Please use: {nameof(ComputerMongoRepository)}")]
    internal class ComputerLocalRepository : IComputerRepository
    {
        public void AddComputer(Computer computer)
        {
            StaticDb.Computers.Add(computer);
        }

        public void DeleteComputer(Guid? id)
        {
            StaticDb.Computers.RemoveAll(c => c.Id == id);
        }

        public List<Computer> GetAllComputers()
        {
            return StaticDb.Computers;
        }

        public Computer? GetById(Guid? id)
        {
            return StaticDb.Computers
                .FirstOrDefault(c =>
                c.Id == id);
        }

        public void UpdateComputer(Computer computer)
        {
            var existingComputer = GetById(computer.Id);
            if (existingComputer != null)
            {
                existingComputer.Brand = computer.Brand;
                existingComputer.Cpu = computer.Cpu;
                existingComputer.Ram = computer.Ram;
                existingComputer.Storage = computer.Storage;
                existingComputer.Gpu = computer.Gpu;
                existingComputer.Category = computer.Category;
                existingComputer.BasePrice = computer.BasePrice;
            }
        }
    }
}
