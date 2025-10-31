using DogsWebApi.Core.Domain.Entities;
using DogsWebApi.Core.Domain.RepositoryContracts;
using DogsWebApi.Core.DTO;
using DogsWebApi.Core.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DogsWebApi.ServiceTests
{
    public class DogServiceTests
    {
        [Fact]
        public async Task CreateAsync_ShouldReturnDogResponse_WithCorrectData()
        {
            // Arrange
            var mockRepo = new Mock<IDogsRepository>();
            var mockLogger = new Mock<ILogger<DogService>>();

            var dogToAdd = new DogAddRequest
            {
                Name = "Rex",
                Color = "Brown",
                Weight = 10,
                TailLength = 5
            };

            var dogEntity = new Dog
            {
                DogID = new Guid(),
                Name = dogToAdd.Name,
                Color = dogToAdd.Color,
                Weight = dogToAdd.Weight,
                TailLength = dogToAdd.TailLength
            };

            mockRepo.Setup(r => r.AddDog(It.IsAny<Dog>()))
                    .Returns(Task.CompletedTask);

            mockRepo.Setup(r => r.SaveChangesAsync())
                    .Returns(Task.CompletedTask);

            var service = new DogService(mockRepo.Object, mockLogger.Object);

            // Act
            var result = await service.CreateAsync(dogToAdd);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Rex");
            result.Color.Should().Be("Brown");
        }
    }
}
