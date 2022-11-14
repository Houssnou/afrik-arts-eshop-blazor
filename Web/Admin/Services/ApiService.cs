using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Admin.Data.ApiResponse;
using Admin.Data.ProductOrigin;
using Admin.Data.ProductType;
using FluentResults;
using Microsoft.AspNetCore.Components;

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

    public async Task<Result<List<ProductType>>> GetAllProductTypes()
    {
        var response = await _httpClient.GetAsync("products/types");
        response.EnsureSuccessStatusCode();

        await using var responseContent = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<BaseResponse<List<ProductType>>>(responseContent, _options);

        //var responseContent = await response.Content.ReadAsStringAsync();
        //var data =  JsonSerializer.Deserialize<List<ProductType>>(responseContent, _options);
        return !result.Success ? Result.Fail(result.Errors) : Result.Ok(result.Data);
    }

    public async Task<Result<ProductType>> CreateNewProductType(ProductType productType)
    {
        var body = new StringContent(JsonSerializer.Serialize(productType), Encoding.UTF8, MediaTypeNames.Application.Json);

        using var httpResponseMessage =
            await _httpClient.PostAsync("products/types", body);

        var responseContent = await httpResponseMessage.Content.ReadAsStreamAsync(); ;

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            var eResponse = await JsonSerializer.DeserializeAsync<BaseResponse<string>>(responseContent, _options);
            return Result.Fail(eResponse.Errors);
        }

        var sResponse = await JsonSerializer.DeserializeAsync<BaseResponse<ProductType>>(responseContent, _options);
        return Result.Ok(sResponse.Data);
    }

    public async Task<Result> DeleteProductType(int id)
    {
        using var httpResponseMessage =
            await _httpClient.DeleteAsync($"products/types/{id}");

        var responseContent = await httpResponseMessage.Content.ReadAsStreamAsync(); ;

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            var eResponse = await JsonSerializer.DeserializeAsync<BaseResponse<string>>(responseContent, _options);
            return Result.Fail(eResponse.Errors);
        }
        return Result.Ok();
    }

    public async Task<Result<List<ProductOrigin>>> GetAllProductOrgins()
    {
        var response = await _httpClient.GetAsync("products/origins");
        response.EnsureSuccessStatusCode();

        await using var responseContent = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<BaseResponse<List<ProductOrigin>>>(responseContent, _options);

        return !result.Success ? Result.Fail(result.Errors) : Result.Ok(result.Data);
    }

    public async Task<Result<ProductOrigin>> CreateNewProductOrigin(ProductOrigin productOrigin)
    {
        var body = new StringContent(JsonSerializer.Serialize(productOrigin), Encoding.UTF8, MediaTypeNames.Application.Json);

        using var httpResponseMessage =
            await _httpClient.PostAsync("products/origins", body);

        var responseContent = await httpResponseMessage.Content.ReadAsStreamAsync(); ;

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            var eResponse = await JsonSerializer.DeserializeAsync<BaseResponse<string>>(responseContent, _options);
            return Result.Fail(eResponse.Errors);
        }

        var sResponse = await JsonSerializer.DeserializeAsync<BaseResponse<ProductOrigin>>(responseContent, _options);
        return Result.Ok(sResponse.Data);
    }

    public async Task<Result> DeleteProductOrigin(int id)
    {
        using var httpResponseMessage =
            await _httpClient.DeleteAsync($"products/origins/{id}");

        var responseContent = await httpResponseMessage.Content.ReadAsStreamAsync(); ;

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            var eResponse = await JsonSerializer.DeserializeAsync<BaseResponse<string>>(responseContent, _options);
            return Result.Fail(eResponse.Errors);
        }
        return Result.Ok();
    }
}