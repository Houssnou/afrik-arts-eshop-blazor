using System.Net.Http;
using System.Text.Json;
using Admin.Data.ProductType;

namespace Admin.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IList<ProductType>?> GetAllProductTypes()
    {
        var response = await _httpClient.GetAsync("products/types");
        response.EnsureSuccessStatusCode();

        await using var responseContent = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<List<ProductType>>(responseContent, _options);

        //var responseContent = await response.Content.ReadAsStringAsync();
        //var data =  JsonSerializer.Deserialize<List<ProductType>>(responseContent, _options);
        //return data;
    }
}