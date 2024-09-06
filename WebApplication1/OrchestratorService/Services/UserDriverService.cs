using System.Text;
using System.Text.Json;
using DefaultNamespace;
using DriverService1.DTO;
using OrchestratorService.DTOs;

namespace OrchestratorService.Services;

public class UserDriverService
{
    private readonly HttpClient _httpClient;
    
    public UserDriverService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<int> CreateUserAndGetUserId(CreateUserDto userDto)
    {
        var userContent = new StringContent(JsonSerializer.Serialize(userDto), Encoding.UTF8, "application/json");
        var userResponse = await _httpClient.PostAsync("http://localhost:5001/api/Users", userContent);

        if (!userResponse.IsSuccessStatusCode)
        {
            throw new Exception("Error creating user.");
        }

        var userResponseContent = await userResponse.Content.ReadAsStringAsync();
        var userResponseDto = JsonSerializer.Deserialize<UserResponseDto>(userResponseContent);
            
        return userResponseDto.UserId;
    }
    
    public async Task CreateDriver(CreateDriverDTO driverDto)
    {
        var driverContent = new StringContent(JsonSerializer.Serialize(driverDto), Encoding.UTF8, "application/json");
        var driverResponse = await _httpClient.PostAsync("http://localhost:5010/api/Driver", driverContent);

        if (!driverResponse.IsSuccessStatusCode)
        {
            throw new Exception("Error creating driver.");
        }
    }
    public async Task CreateUserAndDriver(CreateUserDto userDto, CreateDriverDTO driverDto)
    {
        var userId = await CreateUserAndGetUserId(userDto);
        driverDto.UserId = userId;
        await CreateDriver(driverDto);
    }
}