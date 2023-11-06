using AffiliateProgramManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace AffiliateProgramManagementSystem.Test.IntegrationTests;

public class ApmsBaseTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    protected readonly HttpClient _client;
    protected readonly CustomWebApplicationFactory<Program> _factory;

    public ApmsBaseTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    protected async Task<AffiliateDto> CreateAffiliate(string name)
    {
        var affiliateRequest = new
        {
            Name = name
        };

        var createAffiliateResponse = await _client.PostAsJsonAsync("/api/Affiliates", affiliateRequest);

        Assert.NotNull(createAffiliateResponse);

        createAffiliateResponse.EnsureSuccessStatusCode();

        var affiliate = await createAffiliateResponse.Content.ReadFromJsonAsync<AffiliateDto>();

        Assert.NotNull(affiliate);
        Assert.True(affiliate.Id > 0);

        return affiliate;
    }

    protected async Task<CustomerDto> CreateCustomer(string name, int affiliateId)
    {
        var customerRequest = new
        {
            Name = name,
            AffiliateId = 0
        };

        customerRequest = customerRequest with { AffiliateId = affiliateId };

        var createCustomerResponse = await _client.PostAsJsonAsync("/api/Customers", customerRequest);

        createCustomerResponse.EnsureSuccessStatusCode();

        var customer = await createCustomerResponse.Content.ReadFromJsonAsync<CustomerDto>();

        Assert.NotNull(customer);

        Assert.True(customer.Id > 0);

        return customer;
    }
}
