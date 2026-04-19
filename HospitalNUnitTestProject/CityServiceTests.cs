using Hospital.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Moq;
using NUnit.Framework;

namespace Hospital.Tests.Services
{
    [TestFixture]
    public class CityServiceTests
    {
        private Mock<IWebHostEnvironment> envMock;
        private string tempFolder;
        private CityService service;

        [SetUp]
        public void Setup()
        {
            envMock = new Mock<IWebHostEnvironment>();

            tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempFolder);

            envMock.Setup(x => x.WebRootPath).Returns(tempFolder);

            service = new CityService(envMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }
        }

        private string CreateCitiesFile(string content)
        {
            var dataFolder = Path.Combine(tempFolder, "data");
            Directory.CreateDirectory(dataFolder);

            var filePath = Path.Combine(dataFolder, "cities.json");
            File.WriteAllText(filePath, content);

            return filePath;
        }

        [Test]
        public async Task GetAllAsync_WhenFileExists_ReturnsCities()
        {
            CreateCitiesFile(@"[""Sofia"", ""Plovdiv"", ""Varna""]");

            var result = await service.GetAllAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result.Contains("Sofia"), Is.True);
            Assert.That(result.Contains("Plovdiv"), Is.True);
            Assert.That(result.Contains("Varna"), Is.True);
        }

        [Test]
        public async Task GetAllAsync_WhenFileDoesNotExist_ReturnsEmptyList()
        {
            var result = await service.GetAllAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllAsync_WhenFileIsEmpty_ReturnsEmptyList()
        {
            CreateCitiesFile("");

            var result = await service.GetAllAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetAllAsync_WhenJsonIsInvalid_ReturnsEmptyList()
        {
            CreateCitiesFile("invalid json");

            var result = await service.GetAllAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}