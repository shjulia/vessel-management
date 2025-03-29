using API.Commands.Vessels;
using DomainModel.Entities.Vessels;
using FluentAssertions;
using Moq;

namespace Tests.UnitTests;

public class UpdateVesselHandlerTests
{
    private readonly Mock<IVesselRepository> _vesselRepositoryMock = new ();
    private readonly UpdateVesselHandler _handler;

    public UpdateVesselHandlerTests()
    {
        _handler = new UpdateVesselHandler(_vesselRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenImoIsUnchanged_ShouldUpdateVessel()
    {
        // Arrange
        var vesselId = Guid.NewGuid();
        const string newName = "Updated Vessel";
        var imo = new IMO("1234567");
        var command = new UpdateVesselCommand(newName, imo.Value, VesselType.Cargo.ToString(), 75000) { Id = vesselId };
        var vessel = new Vessel(vesselId, "Old Vessel", imo, VesselType.Tanker, 50000);

        _vesselRepositoryMock.Setup(repo => repo.GetById(vesselId, CancellationToken.None))
            .ReturnsAsync(vessel);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        vessel.Name.Should().Be(newName);
        vessel.Capacity.Should().Be(75000);
        vessel.Type.Should().Be(VesselType.Cargo);
        _vesselRepositoryMock.Verify(repo => repo.Save(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenImoAlreadyExists_ShouldThrowException()
    {
        // Arrange
        var vesselId = Guid.NewGuid();
        var existingImo = new IMO("9999999");
        var command = new UpdateVesselCommand("Updated Vessel", existingImo.Value, VesselType.Cargo.ToString(), 75000) { Id = vesselId };
        var existingVessel = new Vessel(Guid.NewGuid(), "Existing Vessel", existingImo, VesselType.Cargo, 60000);
        var vessel = new Vessel(vesselId, "Old Vessel", new IMO("1234567"), VesselType.Tanker, 50000);

        _vesselRepositoryMock.Setup(repo => repo.GetById(vesselId, CancellationToken.None))
            .ReturnsAsync(vessel);
        _vesselRepositoryMock.Setup(repo => repo.FindByImo(existingImo, CancellationToken.None))
            .ReturnsAsync(existingVessel);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"A vessel with IMO {existingImo.Value} already exists.");

        _vesselRepositoryMock.Verify(repo => repo.Save(CancellationToken.None), Times.Never);
    }
}