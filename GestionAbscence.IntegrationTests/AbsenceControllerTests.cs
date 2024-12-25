using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

public class AbsenceControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public AbsenceControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateAbsence_ReturnsCreatedResponse()
    {
        // Arrange
        var newAbsence = new CreateAbsenceDTO
        {
            EtudiantId = 1,
            MatiereId = 1,
            DateAbsence = DateTime.Now
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/absences", newAbsence);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
} 