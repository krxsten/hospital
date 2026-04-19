using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Core.Services;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Text;

namespace Hospital.Tests.Services
{
    [TestFixture]
    public class DiagnoseServiceTests
    {
        private HospitalDbContext context;
        private Mock<IImageService> imageServiceMock;
        private DiagnoseService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HospitalDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new HospitalDbContext(options);
            imageServiceMock = new Mock<IImageService>();
            service = new DiagnoseService(context, imageServiceMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllDiagnoses()
        {
            context.Diagnoses.AddRange(
                new Diagnose
                {
                    ID = Guid.NewGuid(),
                    Name = "Flu",
                    ImageURL = "/images/flu.jpg",
                    PublicID = "flu_public"
                },
                new Diagnose
                {
                    ID = Guid.NewGuid(),
                    Name = "Cold",
                    ImageURL = "/images/cold.jpg",
                    PublicID = "cold_public"
                });

            await context.SaveChangesAsync();

            var result = await service.GetAllAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(x => x.Name == "Flu"), Is.True);
            Assert.That(result.Any(x => x.Name == "Cold"), Is.True);
        }
        [Test]
        public async Task CreateAsync_AddsDiagnoseToDatabase()
        {
            var fileMock = new Mock<IFormFile>();

            imageServiceMock
                .Setup(x => x.UploadImageAsync(fileMock.Object))
                .ReturnsAsync(("/images/uploaded.jpg", "uploaded_public_id"));

            var model = new DiagnoseCreateDTO
            {
                Name = "Asthma",
                ImageFile = fileMock.Object
            };

            await service.CreateAsync(model);

            var diagnose = await context.Diagnoses.FirstOrDefaultAsync();

            Assert.That(diagnose, Is.Not.Null);
            Assert.That(diagnose!.Name, Is.EqualTo("Asthma"));
            Assert.That(diagnose.ImageURL, Is.EqualTo("/images/uploaded.jpg"));
            Assert.That(diagnose.PublicID, Is.EqualTo("uploaded_public_id"));

            imageServiceMock.Verify(x => x.UploadImageAsync(fileMock.Object), Times.Once);
        }
        [Test]
        public async Task UpdateAsync_WhenFolderDoesNotExist_CreatesFolderAndSavesImage()
        {
            var diagnose = new Diagnose
            {
                ID = Guid.NewGuid(),
                Name = "Test",
                ImageURL = "/images/old.jpg",
                PublicID = "old_id"
            };

            context.Diagnoses.Add(diagnose);
            await context.SaveChangesAsync();

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }

            var fileMock = new Mock<IFormFile>();

            var fileName = "test.jpg";
            var content = "dummy content";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), default))
                .Returns<Stream, CancellationToken>((s, t) =>
                {
                    stream.Position = 0;
                    return stream.CopyToAsync(s);
                });

            var model = new DiagnoseIndexDTO
            {
                ID = diagnose.ID,
                Name = "Updated",
                NewImageFile = fileMock.Object
            };

            await service.UpdateAsync(model);
            Assert.That(Directory.Exists(folderPath), Is.True);
            var updated = await context.Diagnoses.FindAsync(diagnose.ID);
            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.Name, Is.EqualTo("Updated"));
            Assert.That(updated.ImageURL, Does.StartWith("/images/"));
        }
        [Test]
        public async Task GetByIdAsync_WhenDiagnoseExists_ReturnsDiagnose()
        {
            var diagnose = new Diagnose
            {
                ID = Guid.NewGuid(),
                Name = "Diabetes",
                ImageURL = "/images/diabetes.jpg",
                PublicID = "diabetes_public"
            };

            context.Diagnoses.Add(diagnose);
            await context.SaveChangesAsync();

            var result = await service.GetByIdAsync(diagnose.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ID, Is.EqualTo(diagnose.ID));
            Assert.That(result.Name, Is.EqualTo("Diabetes"));
            Assert.That(result.ImageURL, Is.EqualTo("/images/diabetes.jpg"));
        }

        [Test]
        public async Task GetByIdAsync_WhenDiagnoseMissing_ReturnsNull()
        {
            var result = await service.GetByIdAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }


        [Test]
        public async Task UpdateAsync_WhenDiagnoseExistsAndNoNewImage_UpdatesOnlyName()
        {
            var diagnose = new Diagnose
            {
                ID = Guid.NewGuid(),
                Name = "Old Name",
                ImageURL = "/images/old.jpg",
                PublicID = "old_public"
            };

            context.Diagnoses.Add(diagnose);
            await context.SaveChangesAsync();

            var dto = new DiagnoseIndexDTO
            {
                ID = diagnose.ID,
                Name = "New Name",
                ImageURL = diagnose.ImageURL,
                NewImageFile = null
            };

            await service.UpdateAsync(dto);

            var updated = await context.Diagnoses.FindAsync(diagnose.ID);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.Name, Is.EqualTo("New Name"));
            Assert.That(updated.ImageURL, Is.EqualTo("/images/old.jpg"));
        }

        [Test]
        public async Task UpdateAsync_WhenDiagnoseExistsAndNewImage_UpdatesNameAndImage()
        {
            var diagnose = new Diagnose
            {
                ID = Guid.NewGuid(),
                Name = "Old Name",
                ImageURL = "/images/old.jpg",
                PublicID = "old_public"
            };

            context.Diagnoses.Add(diagnose);
            await context.SaveChangesAsync();

            var content = "fake image content";
            var fileName = "newImage.jpg";
            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));

            IFormFile formFile = new FormFile(stream, 0, stream.Length, "Data", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            var dto = new DiagnoseIndexDTO
            {
                ID = diagnose.ID,
                Name = "Updated Diagnose",
                NewImageFile = formFile
            };

            await service.UpdateAsync(dto);

            var updated = await context.Diagnoses.FindAsync(diagnose.ID);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.Name, Is.EqualTo("Updated Diagnose"));
            Assert.That(updated.ImageURL, Does.StartWith("/images/"));
            Assert.That(updated.ImageURL, Does.EndWith(".jpg"));
        }

        [Test]
        public void UpdateAsync_WhenDiagnoseMissing_ThrowsException()
        {
            var dto = new DiagnoseIndexDTO
            {
                ID = Guid.NewGuid(),
                Name = "Missing Diagnose"
            };

            var ex = Assert.ThrowsAsync<Exception>(async () => await service.UpdateAsync(dto));

            Assert.That(ex!.Message, Is.EqualTo("Diagnose not found"));
        }

        [Test]
        public async Task DeleteAsync_WhenDiagnoseExists_RemovesDiagnose()
        {
            var diagnose = new Diagnose
            {
                ID = Guid.NewGuid(),
                Name = "To Delete",
                ImageURL = "/images/delete.jpg",
                PublicID = "delete_public"
            };

            context.Diagnoses.Add(diagnose);
            await context.SaveChangesAsync();

            await service.DeleteAsync(diagnose.ID);

            var deleted = await context.Diagnoses.FindAsync(diagnose.ID);
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_WhenDiagnoseMissing_DoesNothing()
        {
            await service.DeleteAsync(Guid.NewGuid());

            Assert.That(await context.Diagnoses.CountAsync(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetDiagnose_WhenSearchTermIsNull_ReturnsEmptyList()
        {
            var result = await service.GetDiagnose(null!);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetDiagnose_WhenSearchTermIsWhitespace_ReturnsEmptyList()
        {
            var result = await service.GetDiagnose("   ");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetDiagnose_WhenMatchesExist_ReturnsMatchingDiagnoses()
        {
            context.Diagnoses.AddRange(
                new Diagnose
                {
                    ID = Guid.NewGuid(),
                    Name = "Heart Disease",
                    ImageURL = "/images/heart.jpg",
                    PublicID = "heart_public"
                },
                new Diagnose
                {
                    ID = Guid.NewGuid(),
                    Name = "Lung Disease",
                    ImageURL = "/images/lung.jpg",
                    PublicID = "lung_public"
                },
                new Diagnose
                {
                    ID = Guid.NewGuid(),
                    Name = "Flu",
                    ImageURL = "/images/flu.jpg",
                    PublicID = "flu_public"
                });

            await context.SaveChangesAsync();

            var result = await service.GetDiagnose("Disease");

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(x => x.Name == "Heart Disease"), Is.True);
            Assert.That(result.Any(x => x.Name == "Lung Disease"), Is.True);
            Assert.That(result.Any(x => x.Name == "Flu"), Is.False);
        }

        [Test]
        public async Task GetDiagnose_WhenNoMatchesExist_ReturnsEmptyList()
        {
            context.Diagnoses.Add(
                new Diagnose
                {
                    ID = Guid.NewGuid(),
                    Name = "Flu",
                    ImageURL = "/images/flu.jpg",
                    PublicID = "flu_public"
                });

            await context.SaveChangesAsync();

            var result = await service.GetDiagnose("Cancer");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}