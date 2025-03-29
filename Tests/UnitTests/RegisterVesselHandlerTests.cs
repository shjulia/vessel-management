using API.Commands.Vessels;
using DomainModel.Entities.Vessels;
using FluentAssertions;
using Moq;

namespace Tests.UnitTests;

public class RegisterVesselHandlerTests
{
    private readonly Mock<IVesselRepository> _vesselRepositoryMock = new ();
    private readonly RegisterVesselHandler _handler;

    public RegisterVesselHandlerTests()
    {
        _handler = new RegisterVesselHandler(_vesselRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenImoIsUnique_ShouldRegisterVessel()
    {
        // Arrange
        var imo = new IMO("1234567");
        var command = new RegisterVesselCommand("Test Vessel", imo.Value, VesselType.Cargo.ToString(), 50000);
        _vesselRepositoryMock.Setup(repo => repo.FindByImo(imo, CancellationToken.None))
            .ReturnsAsync((Vessel)null); // No vessel with the given IMO exists
        
        var vesselId = Guid.NewGuid();
        _vesselRepositoryMock.Setup(repo => repo.Add(It.IsAny<Vessel>()))
            .Callback<Vessel>(v => vesselId = v.Id);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeEmpty();
        _vesselRepositoryMock.Verify(repo => repo.Add(It.IsAny<Vessel>()), Times.Once);
        _vesselRepositoryMock.Verify(repo => repo.Save(CancellationToken.None), Times.Once);
        result.Should().Be(vesselId);
    }

    [Fact]
    public async Task Handle_WhenImoAlreadyExists_ShouldThrowException()
    {
        // Arrange
        var imo = new IMO("1234567");
        var command = new RegisterVesselCommand("Test Vessel", imo.Value, VesselType.Cargo.ToString(), 50000);
        var existingVessel = new Vessel(Guid.NewGuid(), "Existing Vessel", imo, VesselType.Cargo, 60000);
        
        _vesselRepositoryMock.Setup(repo => repo.FindByImo(imo, CancellationToken.None))
            .ReturnsAsync(existingVessel);
        
        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"A vessel with IMO {imo.Value} already exists.");
        
        _vesselRepositoryMock.Verify(repo => repo.Add(It.IsAny<Vessel>()), Times.Never);
        _vesselRepositoryMock.Verify(repo => repo.Save(CancellationToken.None), Times.Never);
    }
}