using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Hospital.Core.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System.Text;

namespace Hospital.Tests.Services
{
    [TestFixture]
    public class ImageServiceTests
    {

        private Mock<Cloudinary> cloudinaryMock;
        private ImageService service;

        [SetUp]
        public void Setup()
        {
            cloudinaryMock = new Mock<Cloudinary>(new Account("cloud", "apiKey", "apiSecret"));
           // service = new ImageService(cloudinaryMock.Object);
        }

        private IFormFile CreateFormFile(string content, string fileName, string contentType)
        {
            var bytes = Encoding.UTF8.GetBytes(content);
            var stream = new MemoryStream(bytes);

            return new FormFile(stream, 0, bytes.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };
        }

        [Test]
        public void UploadImageAsync_WhenFileIsNull_ThrowsArgumentException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await service.UploadImageAsync(null!));

            Assert.That(ex!.Message, Is.EqualTo("File is empty or null!"));
        }

        [Test]
        public void UploadImageAsync_WhenFileIsEmpty_ThrowsArgumentException()
        {
            var stream = new MemoryStream();
            IFormFile file = new FormFile(stream, 0, 0, "file", "empty.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await service.UploadImageAsync(file));

            Assert.That(ex!.Message, Is.EqualTo("File is empty or null!"));
        }

        [Test]
        public void UploadImageAsync_WhenFileTypeIsInvalid_ThrowsArgumentException()
        {
            var file = CreateFormFile("fake file", "test.gif", "image/gif");

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await service.UploadImageAsync(file));

            Assert.That(ex!.Message, Is.EqualTo("Invalid file type. Only JPG, PNG, and WEBP are allowed."));
        }

        [Test]
        public async Task UploadImageAsync_WhenUploadSucceeds_ReturnsUrlAndPublicId()
        {
            var file = CreateFormFile("fake image content", "test.jpg", "image/jpeg");

            var uploadResult = new ImageUploadResult
            {
                SecureUrl = new Uri("https://res.cloudinary.com/demo/image/upload/test.jpg"),
                PublicId = "hospital_uploads/test"
            };

            //cloudinaryMock
            //    .Setup(x => x.UploadAsync(It.IsAny<ImageUploadParams>()))
            //    .ReturnsAsync(uploadResult);

            var result = await service.UploadImageAsync(file);

            Assert.That(result.Url, Is.EqualTo("https://res.cloudinary.com/demo/image/upload/test.jpg"));
            Assert.That(result.PublicId, Is.EqualTo("hospital_uploads/test"));

         //   cloudinaryMock.Verify(x => x.UploadAsync(It.IsAny<ImageUploadParams>()), Times.Once);
        }

        [Test]
        public void UploadImageAsync_WhenCloudinaryReturnsError_ThrowsException()
        {
            var file = CreateFormFile("fake image content", "test.jpg", "image/jpeg");

            var uploadResult = new ImageUploadResult
            {
                Error = new Error { Message = "Upload failed" }
            };

            //cloudinaryMock
            //    .Setup(x => x.UploadAsync(It.IsAny<ImageUploadParams>()))
            //    .ReturnsAsync(uploadResult);

            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await service.UploadImageAsync(file));

            Assert.That(ex!.Message, Is.EqualTo("Cloudinary Upload Failed: Upload failed"));
        }

        [Test]
        public async Task DestroyImageAsync_WhenPublicIdIsNull_DoesNothing()
        {
            await service.DestroyImageAsync(null!);

            cloudinaryMock.Verify(x => x.DestroyAsync(It.IsAny<DeletionParams>()), Times.Never);
        }

        [Test]
        public async Task DestroyImageAsync_WhenPublicIdIsEmpty_DoesNothing()
        {
            await service.DestroyImageAsync(string.Empty);

            cloudinaryMock.Verify(x => x.DestroyAsync(It.IsAny<DeletionParams>()), Times.Never);
        }

        [Test]
        public async Task DestroyImageAsync_WhenPublicIdIsValid_CallsCloudinaryDestroy()
        {
         //   var service = new ImageService(cloudinaryMock.Object);

            cloudinaryMock
                .Setup(x => x.DestroyAsync(It.IsAny<DeletionParams>()))
                .ReturnsAsync(new DeletionResult { Result = "ok" });

            await service.DestroyImageAsync("hospital_uploads/test");

            cloudinaryMock.Verify(
                x => x.DestroyAsync(It.Is<DeletionParams>(p => p.PublicId == "hospital_uploads/test")),
                Times.Once);
        }
    }
}