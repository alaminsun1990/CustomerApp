using System.Net;
using System.Net.Http.Json;
using CustomerApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CustomerApi.Tests;

public class CustomerApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CustomerApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Customers_ReturnsSuccess()
    {
        var response = await _client.GetAsync("/api/customers");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Post_And_Get_Customer_Works()
    {
        var customer = new Customer
        {
            FirstName = "Jane",
            LastName = "Doe",
            Email = $"jane{Guid.NewGuid()}@test.com",
            PhoneNumber = "1234567890"
        };

        var postResponse = await _client.PostAsJsonAsync("/api/customers", customer);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var createdCustomer = await postResponse.Content.ReadFromJsonAsync<Customer>();
        var getResponse = await _client.GetAsync($"/api/customers/{createdCustomer!.Id}");

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        var fetched = await getResponse.Content.ReadFromJsonAsync<Customer>();
        Assert.Equal(customer.FirstName, fetched!.FirstName);
    }

    [Fact]
    public async Task Put_Updates_Customer_Successfully()
    {
        var customer = new Customer
        {
            FirstName = "Mark",
            LastName = "Smith",
            Email = $"mark{Guid.NewGuid()}@test.com",
            PhoneNumber = "1112223333"
        };

        // Create
        var postResponse = await _client.PostAsJsonAsync("/api/customers", customer);
        var createdCustomer = await postResponse.Content.ReadFromJsonAsync<Customer>();

        // Update
        createdCustomer!.LastName = "Johnson";
        var putResponse = await _client.PutAsJsonAsync($"/api/customers/{createdCustomer.Id}", createdCustomer);

        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

        // Verify update
        var updatedCustomer = await _client.GetFromJsonAsync<Customer>($"/api/customers/{createdCustomer.Id}");
        Assert.Equal("Johnson", updatedCustomer!.LastName);
    }

    [Fact]
    public async Task Delete_Removes_Customer_Successfully()
    {
        var customer = new Customer
        {
            FirstName = "Lisa",
            LastName = "Brown",
            Email = $"lisa{Guid.NewGuid()}@test.com",
            PhoneNumber = "5556667777"
        };

        // Create
        var postResponse = await _client.PostAsJsonAsync("/api/customers", customer);
        var createdCustomer = await postResponse.Content.ReadFromJsonAsync<Customer>();

        // Delete
        var deleteResponse = await _client.DeleteAsync($"/api/customers/{createdCustomer!.Id}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // Verify deletion
        var getResponse = await _client.GetAsync($"/api/customers/{createdCustomer.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task GetCustomer_WithInvalidId_ReturnsNotFound()
    {
        var invalidId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/customers/{invalidId}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task PostCustomer_WithMissingFields_ReturnsBadRequest()
    {
        var incompleteCustomer = new Customer
        {
            FirstName = "", // Required but empty
            Email = "test@test.com"
        };

        var response = await _client.PostAsJsonAsync("/api/customers", incompleteCustomer);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostCustomer_WithDuplicateEmail_ReturnsError()
    {
        var email = $"duplicate{Guid.NewGuid()}@test.com";

        var customer1 = new Customer { FirstName = "Test", LastName = "One", Email = email, PhoneNumber = "123" };
        var customer2 = new Customer { FirstName = "Test", LastName = "Two", Email = email, PhoneNumber = "456" };

        await _client.PostAsJsonAsync("/api/customers", customer1);
        var response = await _client.PostAsJsonAsync("/api/customers", customer2);

        Assert.True(response.StatusCode == HttpStatusCode.Conflict || (int)response.StatusCode >= 400);
    }


}
