using System.Net.Http.Headers;
using System.Text.Json;

namespace ShipmentService.Services.Implementation;

public class CustomerServiceClient : ICustomerService
{
    private readonly HttpClient _httpClient;
    public CustomerServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<int?> GetCustomerIdByTokenAsync(string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/Customer/id");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var customerId = JsonSerializer.Deserialize<int?>(responseContent);
        return customerId;
    }
}