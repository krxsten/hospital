using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Core.Services;
using Hospital.Data;
using Hospital.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Hospital.Tests.Services
{
    [TestFixture]
    public class SpecializationServiceTests
    {
        private HospitalDbContext context;
        private Mock<IImageService> imageServiceMock;
        private SpecializationService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HospitalDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new HospitalDbContext(options);
            imageServiceMock = new Mock<IImageService>();
            service = new SpecializationService(context, imageServiceMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private async Task<Specialization> SeedSpecializationAsync(
            string name = "Cardiology",
            string imageUrl = "/images/cardio.jpg",
            string publicId = "cardio_public")
        {
            var specialization = new Specialization
            {
                ID = Guid.NewGuid(),
                SpecializationName = name,
                ImageURL = imageUrl,
                PublicID = publicId
            };

            context.Specializations.Add(specialization);
            await context.SaveChangesAsync();

            return specialization;
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllSpecializations()
        {
            await SeedSpecializationAsync("Cardiology");
            await SeedSpecializationAsync("Neurology");

            var result = await service.GetAllAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(x => x.SpecializationName == "Cardiology"), Is.True);
            Assert.That(result.Any(x => x.SpecializationName == "Neurology"), Is.True);
        }

        [Test]
        public async Task GetByIdAsync_WhenSpecializationExists_ReturnsSpecialization()
        {
            var specialization = await SeedSpecializationAsync("Surgery", "/images/surgery.jpg", "surgery_public");

            var result = await service.GetByIdAsync(specialization.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ID, Is.EqualTo(specialization.ID));
            Assert.That(result.SpecializationName, Is.EqualTo("Surgery"));
            Assert.That(result.ImageURL, Is.EqualTo("/images/surgery.jpg"));
        }

        [Test]
        public async Task GetByIdAsync_WhenSpecializationMissing_ReturnsNull()
        {
            var result = await service.GetByIdAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task CreateAsync_AddsSpecializationToDatabase()
        {
            var fileMock = new Mock<IFormFile>();

            imageServiceMock
                .Setup(x => x.UploadImageAsync(fileMock.Object))
                .ReturnsAsync(("/images/uploaded.jpg", "uploaded_public_id"));

            var model = new SpecializationCreateDTO
            {
                SpecializationName = "Dermatology",
                ImageFile = fileMock.Object
            };

            await service.CreateAsync(model);

            var specialization = await context.Specializations.FirstOrDefaultAsync();

            Assert.That(specialization, Is.Not.Null);
            Assert.That(specialization!.SpecializationName, Is.EqualTo("Dermatology"));
            Assert.That(specialization.ImageURL, Is.EqualTo("/images/uploaded.jpg"));
            Assert.That(specialization.PublicID, Is.EqualTo("uploaded_public_id"));

            imageServiceMock.Verify(x => x.UploadImageAsync(fileMock.Object), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_WhenSpecializationExistsAndNoNewImage_UpdatesOnlyName()
        {
            var specialization = await SeedSpecializationAsync("Cardiology", "/images/cardio.jpg", "cardio_public");

            var model = new SpecializationIndexDTO
            {
                ID = specialization.ID,
                SpecializationName = "Updated Cardiology",
                ImageURL = specialization.ImageURL,
                NewImageFile = null
            };

            await service.UpdateAsync(model);

            var updated = await context.Specializations.FindAsync(specialization.ID);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.SpecializationName, Is.EqualTo("Updated Cardiology"));
            Assert.That(updated.ImageURL, Is.EqualTo("/images/cardio.jpg"));
            Assert.That(updated.PublicID, Is.EqualTo("cardio_public"));

            imageServiceMock.Verify(x => x.DestroyImageAsync(It.IsAny<string>()), Times.Never);
            imageServiceMock.Verify(x => x.UploadImageAsync(It.IsAny<IFormFile>()), Times.Never);
        }

        [Test]
        public async Task UpdateAsync_WhenSpecializationExistsAndNewImage_UpdatesNameAndImage()
        {
            var specialization = await SeedSpecializationAsync("Cardiology", "/images/cardio.jpg", "cardio_public");
            var fileMock = new Mock<IFormFile>();

            imageServiceMock
                .Setup(x => x.DestroyImageAsync("cardio_public"))
                .Returns(Task.CompletedTask);

            imageServiceMock
                .Setup(x => x.UploadImageAsync(fileMock.Object))
                .ReturnsAsync(("/images/new-specialization.jpg", "new_public_id"));

            var model = new SpecializationIndexDTO
            {
                ID = specialization.ID,
                SpecializationName = "Updated Cardiology",
                NewImageFile = fileMock.Object
            };

            await service.UpdateAsync(model);

            var updated = await context.Specializations.FindAsync(specialization.ID);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.SpecializationName, Is.EqualTo("Updated Cardiology"));
            Assert.That(updated.ImageURL, Is.EqualTo("/images/new-specialization.jpg"));
            Assert.That(updated.PublicID, Is.EqualTo("new_public_id"));

            imageServiceMock.Verify(x => x.DestroyImageAsync("cardio_public"), Times.Once);
            imageServiceMock.Verify(x => x.UploadImageAsync(fileMock.Object), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_WhenSpecializationHasNoPublicIdAndNewImage_UploadsWithoutDestroy()
        {
            var specialization = await SeedSpecializationAsync("Cardiology", "/images/cardio.jpg", "");
            var fileMock = new Mock<IFormFile>();

            imageServiceMock
                .Setup(x => x.UploadImageAsync(fileMock.Object))
                .ReturnsAsync(("/images/new-specialization.jpg", "new_public_id"));

            var model = new SpecializationIndexDTO
            {
                ID = specialization.ID,
                SpecializationName = "Updated Cardiology",
                NewImageFile = fileMock.Object
            };

            await service.UpdateAsync(model);

            var updated = await context.Specializations.FindAsync(specialization.ID);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.SpecializationName, Is.EqualTo("Updated Cardiology"));
            Assert.That(updated.ImageURL, Is.EqualTo("/images/new-specialization.jpg"));
            Assert.That(updated.PublicID, Is.EqualTo("new_public_id"));

            imageServiceMock.Verify(x => x.DestroyImageAsync(It.IsAny<string>()), Times.Never);
            imageServiceMock.Verify(x => x.UploadImageAsync(fileMock.Object), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_WhenSpecializationMissing_DoesNothing()
        {
            var model = new SpecializationIndexDTO
            {
                ID = Guid.NewGuid(),
                SpecializationName = "Missing"
            };

            await service.UpdateAsync(model);

            Assert.That(await context.Specializations.CountAsync(), Is.EqualTo(0));
            imageServiceMock.Verify(x => x.DestroyImageAsync(It.IsAny<string>()), Times.Never);
            imageServiceMock.Verify(x => x.UploadImageAsync(It.IsAny<IFormFile>()), Times.Never);
        }

        [Test]
        public async Task DeleteAsync_WhenSpecializationExists_RemovesSpecialization()
        {
            var specialization = await SeedSpecializationAsync();

            await service.DeleteAsync(specialization.ID);

            var deleted = await context.Specializations.FindAsync(specialization.ID);
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_WhenSpecializationMissing_DoesNothing()
        {
            await service.DeleteAsync(Guid.NewGuid());

            Assert.That(await context.Specializations.CountAsync(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetSpecialization_WhenSearchIsNull_ReturnsEmptyList()
        {
            var result = await service.GetSpecialization(null!);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetSpecialization_WhenSearchIsWhitespace_ReturnsEmptyList()
        {
            var result = await service.GetSpecialization("   ");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetSpecialization_WhenMatchesExist_ReturnsMatchingSpecializations()
        {
            await SeedSpecializationAsync("Cardiology");
            await SeedSpecializationAsync("Cardio Surgery");
            await SeedSpecializationAsync("Neurology");

            var result = await service.GetSpecialization("Cardio");

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(x => x.SpecializationName == "Cardiology"), Is.True);
            Assert.That(result.Any(x => x.SpecializationName == "Cardio Surgery"), Is.True);
            Assert.That(result.Any(x => x.SpecializationName == "Neurology"), Is.False);
        }

        [Test]
        public async Task GetSpecialization_WhenNoMatchesExist_ReturnsEmptyList()
        {
            await SeedSpecializationAsync("Cardiology");

            var result = await service.GetSpecialization("Oncology");

            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}