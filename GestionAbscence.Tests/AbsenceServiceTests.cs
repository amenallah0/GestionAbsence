using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using GestionAbscence.Repositories;
using GestionAbscence.Services;

public class AbsenceServiceTests
{
    private readonly Mock<IAbsenceRepository> _repositoryMock;
    private readonly IAbsenceService _service;

    public AbsenceServiceTests()
    {
        _repositoryMock = new Mock<IAbsenceRepository>();
        _service = new AbsenceService(_repositoryMock.Object);
    }

    [Fact]
    public async Task JustifierAbsence_WithValidData_ShouldUpdateAbsence()
    {
        // Arrange
        var absenceId = 1;
        var justification = "Certificat médical";
        var absence = new Absence { Id = absenceId, EstJustifiee = false };

        _repositoryMock.Setup(x => x.GetByIdAsync(absenceId))
            .ReturnsAsync(absence);

        // Act
        await _service.JustifierAbsence(absenceId, justification);

        // Assert
        _repositoryMock.Verify(x => x.UpdateAsync(It.Is<Absence>(
            a => a.Id == absenceId && 
                 a.EstJustifiee && 
                 a.Justification == justification)), 
            Times.Once);
    }
} 