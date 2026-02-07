using ComputerStore.BL.Services;
using ComputerStore.DL.Interfaces;
using ComputerStore.Models.Dto;
using ComputerStore.Tests.MockData;
using Moq;

namespace ComputerStore.Tests.ComputerTests
{
    public class ComputerCrudServiceTests
    {
        private readonly Mock<IComputerRepository> _computerRepositoryMock;

        public ComputerCrudServiceTests()
        {
            _computerRepositoryMock = new Mock<IComputerRepository>();

        }

        [Fact]
        public void AddComputerTest_Ok()
        {
            //setup
            var expectedComputerCount = ComputerMockedData.Computers.Count + 1;
            var id = Guid.NewGuid();
            var computer = new Computer
            {
                Id = id,
                Brand = "Lenovo",
                Cpu = "Intel Core i5-14400F",
                Ram = "16GB DDR4 3200MHz",
                Storage = "1TB NVMe SSD",
                Gpu = "AMD Radeon RX580 8GB GDDR5",
                Category = "Desktop",
                BasePrice = 850m
            };

            Computer resultComputer = null;
            _computerRepositoryMock
                .Setup(repo => repo.AddComputer(computer))
                .Callback(() =>
                {
                    ComputerMockedData.Computers.Add(computer);
                });

            //inject
            var service = new ComputerCrudService(_computerRepositoryMock.Object);

            //act
            service.AddComputer(computer);

            resultComputer = ComputerMockedData.Computers.FirstOrDefault(c => c.Id == id);
            //assert
            Assert.NotNull(resultComputer);
            Assert.Contains(computer, ComputerMockedData.Computers);
            Assert.Equal(expectedComputerCount, ComputerMockedData.Computers.Count);
            Assert.Equal(id, resultComputer.Id);
        }

        [Fact]
        public void AddComputerTest_NotOk_NullComputer()
        {
            // arrange
            var initialCount = ComputerMockedData.Computers.Count;

            var service = new ComputerCrudService(_computerRepositoryMock.Object);

            // act
            service.AddComputer(null);

            // assert
            _computerRepositoryMock.Verify(
                repo => repo.AddComputer(It.IsAny<Computer>()),
                Times.Never);

            Assert.Equal(initialCount, ComputerMockedData.Computers.Count);
        }

        [Fact]
        public void DeleteComputerTest_Ok()
        {
            // setup
            var computerToDelete = ComputerMockedData.Computers.First();
            var initialComputerCount = ComputerMockedData.Computers.Count;

            _computerRepositoryMock
                .Setup(repo => repo.DeleteComputer(computerToDelete.Id))
                .Callback(() =>
                {
                    var computer = ComputerMockedData.Computers.FirstOrDefault(c => c.Id == computerToDelete.Id);
                    if (computer != null)
                    {
                        ComputerMockedData.Computers.Remove(computer);
                    }
                });

            // inject
            var service = new ComputerCrudService(_computerRepositoryMock.Object);

            // act
            service.DeleteComputer(computerToDelete.Id);

            var deletedComputer = ComputerMockedData.Computers.FirstOrDefault(c => c.Id == computerToDelete.Id);

            // assert
            Assert.Null(deletedComputer);
            Assert.Equal(initialComputerCount - 1, ComputerMockedData.Computers.Count);
        }

        [Fact]
        public void DeleteComputerTest_NotOk_NotExistingId()
        {
            // arrange
            var initialCount = ComputerMockedData.Computers.Count;
            var nonExistingId = Guid.NewGuid();

            _computerRepositoryMock
                .Setup(repo => repo.DeleteComputer(nonExistingId));

            var service = new ComputerCrudService(_computerRepositoryMock.Object);

            // act
            service.DeleteComputer(nonExistingId);

            // assert
            Assert.Equal(initialCount, ComputerMockedData.Computers.Count);
        }

        [Fact]
        public void UpdateComputerTest_Ok()
        {
            // setup
            var existingComputer = ComputerMockedData.Computers.First();
            var updatedBrand = "Asus";
            var updatedCpu = "AMD Ryzen 7 260";
            var updatedRam = "16GB DDR5 5600MHz";
            var updatedStorage = "512GB NVMe SSD";
            var updatedGpu = "NVIDIA GeForce RTX 5060 8GB GDDR7";
            var updatedCategory = "Laptop";
            var updatedBasePrice = 1400m;
            var expectedComputerCount = ComputerMockedData.Computers.Count;

            var updatedComputer = new Computer
            {
                Id = existingComputer.Id,
                Brand = updatedBrand,
                Cpu = updatedCpu,
                Ram = updatedRam,
                Storage = updatedStorage,
                Gpu = updatedGpu,
                Category = updatedCategory,
                BasePrice = updatedBasePrice
            };

            _computerRepositoryMock
                .Setup(repo => repo.UpdateComputer(updatedComputer))
                .Callback(() =>
                {
                    var computer = ComputerMockedData.Computers
                        .FirstOrDefault(c => c.Id == updatedComputer.Id);

                    if (computer != null)
                    {
                        computer.Brand = updatedComputer.Brand;
                        computer.Cpu = updatedComputer.Cpu;
                        computer.Ram = updatedComputer.Ram;
                        computer.Storage = updatedComputer.Storage;
                        computer.Gpu = updatedComputer.Gpu;
                        computer.Category = updatedComputer.Category;
                        computer.BasePrice = updatedComputer.BasePrice;
                    }
                });

            // inject
            var service = new ComputerCrudService(_computerRepositoryMock.Object);

            // act
            service.UpdateComputer(updatedComputer);

            var resultComputer = ComputerMockedData.Computers
                .FirstOrDefault(c => c.Id == updatedComputer.Id);

            // assert
            Assert.NotNull(resultComputer);
            Assert.Equal(updatedBrand, resultComputer.Brand);
            Assert.Equal(updatedCpu, resultComputer.Cpu);
            Assert.Equal(updatedRam, resultComputer.Ram);
            Assert.Equal(updatedStorage, resultComputer.Storage);
            Assert.Equal(updatedGpu, resultComputer.Gpu);
            Assert.Equal(updatedCategory, resultComputer.Category);
            Assert.Equal(updatedBasePrice, resultComputer.BasePrice);
            Assert.Equal(expectedComputerCount, ComputerMockedData.Computers.Count);
        }

        [Fact]
        public void UpdateComputerTest_NotOk_NotExistingComputer()
        {
            // arrange
            var initialState = ComputerMockedData.Computers
                .Select(c => c.Brand)
                .ToList();

            var computer = new Computer
            {
                Id = Guid.NewGuid(),
                Brand = "HP",
                Cpu = "AMD Ryzen AI 5 340",
                Ram = "8GB DDR5 5600MHz",
                Storage = "512GB NVMe SSD",
                Gpu = "NVIDIA GeForce RTX 5050 8GB GDDR7",
                Category = "Laptop",
                BasePrice = 1400m
            };

            _computerRepositoryMock
                .Setup(repo => repo.UpdateComputer(computer));

            var service = new ComputerCrudService(_computerRepositoryMock.Object);

            // act
            service.UpdateComputer(computer);

            // assert
            var finalState = ComputerMockedData.Computers
                .Select(c => c.Brand)
                .ToList();

            Assert.Equal(initialState, finalState);
        }
    }
}
