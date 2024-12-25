using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using GestionAbscence.Repositories;
using GestionAbscence.Models;

public class AbsenceRepositoryTests
{
    private readonly Mock<ApplicationDbContext> _contextMock;
    private readonly IAbsenceRepository _repository;

    public AbsenceRepositoryTests()
    {
        _contextMock = new Mock<ApplicationDbContext>();
        _repository = new AbsenceRepository(_contextMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllAbsences()
    {
        // Arrange
        var expectedAbsences = new List<Absence>
        {
            new Absence { Id = 1, EtudiantId = 1, DateAbsence = DateTime.Now },
            new Absence { Id = 2, EtudiantId = 2, DateAbsence = DateTime.Now }
        };

        var dbSetMock = MockDbSet(expectedAbsences);
        _contextMock.Setup(x => x.Absences).Returns(dbSetMock.Object);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.Equal(expectedAbsences.Count, result.Count());
    }
} 