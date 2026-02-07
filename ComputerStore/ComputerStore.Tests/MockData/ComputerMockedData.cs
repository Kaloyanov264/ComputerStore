using ComputerStore.Models.Dto;

namespace ComputerStore.Tests.MockData
{
    internal class ComputerMockedData
    {
        public static List<Computer> Computers = new List<Computer>
        {
            new Computer { Id = Guid.NewGuid(),
                Brand = "Lenovo",
                Cpu = "Intel Core i5-14400F",
                Ram = "16GB DDR4 3200MHz",
                Storage = "1TB NVMe SSD",
                Gpu = "AMD Radeon RX580 8GB GDDR5",
                Category = "Desktop",
                BasePrice = 850},

            new Computer { Id = Guid.NewGuid(),
                Brand = "Dell",
                Cpu = "AMD Ryzen 7 7800X3D",
                Ram = "32GB DDR5 6000MHz",
                Storage = "2TB NVMe SSD",
                Gpu = "AMD RX 9060XT 16GB GDDR6",
                Category = "Desktop",
                BasePrice = 1700},

            new Computer { Id = Guid.NewGuid(),
                Brand = "Acer",
                Cpu = "AMD Ryzen 7 7445HS",
                Ram = "32GB DDR5 5600MHz",
                Storage = "1TB NVMe SSD",
                Gpu = "NVIDIA GeForce RTX 3050 6GB GDDR6",
                Category = "Laptop",
                BasePrice = 1150},
        };
    }
}
