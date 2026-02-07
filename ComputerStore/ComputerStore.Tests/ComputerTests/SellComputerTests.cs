using ComputerStore.BL.Interfaces;
using ComputerStore.DL.Interfaces;
using ComputerStore.Models.Dto;
using Moq;

namespace ComputerStore.Tests.ComputerTests
{
    public class SellComputerTests
    {
        Mock<IComputerCrudService> _computerCrudServiceMock;
        Mock<ICustomerRepository> _customerRepositoryMock;

        [Fact]
        public void Sell_Return_Ok()
        {
            _computerCrudServiceMock = new Mock<IComputerCrudService>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            var expectedPrice = 800m;

            _computerCrudServiceMock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(new Models.Dto.Computer
                {
                    Id = Guid.NewGuid(),
                    Brand = "MSI",
                    Cpu = "Intel Core i7-13620H",
                    Ram = "16GB DDR5 5600MHz",
                    Storage = "1TB NVMe SSD",
                    Gpu = "NVIDIA GeForce RTX 3050 6GB GDDR6",
                    Category = "Laptop",
                    BasePrice = 1000m
                });

            _customerRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(new Models.Dto.Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Martin Todorov",
                    Email = "MarTod@gmail.com",
                    Discount = 200
                });

            var sellService = new BL.Services.SellComputer(_computerCrudServiceMock.Object, _customerRepositoryMock.Object);

            //act
            var result = sellService.Sell(Guid.NewGuid(), Guid.NewGuid());

            //assert
            Assert.NotNull(result);
            Assert.Equal(expectedPrice, result.Price);
        }

        [Fact]
        public void Sell_When_Customer_Missing()
        {
            _computerCrudServiceMock = new Mock<IComputerCrudService>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            var expectedPrice = 800m;

            _computerCrudServiceMock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(new Models.Dto.Computer
                {
                    Id = Guid.NewGuid(),
                    Brand = "MSI",
                    Cpu = "Intel Core i7-13620H",
                    Ram = "16GB DDR5 5600MHz",
                    Storage = "1TB NVMe SSD",
                    Gpu = "NVIDIA GeForce RTX 3050 6GB GDDR6",
                    Category = "Laptop",
                    BasePrice = 1000m
                });

            _customerRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns((Customer)null);

            var sellService = new BL.Services.SellComputer(_computerCrudServiceMock.Object, _customerRepositoryMock.Object);

            //act + assert
            var ex = Assert.Throws<ArgumentException>(() => sellService.Sell(Guid.NewGuid(), Guid.NewGuid()));
            //var result = sellService.Sell(Guid.NewGuid(), Guid.NewGuid());

            //assert
            //Assert.NotNull(result);
            //Assert.Equal(expectedPrice, result.Price);
        }
    }
}
