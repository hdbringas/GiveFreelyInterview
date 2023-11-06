using AffiliateProgramManagementSystem.Models;
using AffiliateProgramManagementSystem.Test.Utils;
using System.Net.Http.Json;

namespace AffiliateProgramManagementSystem.Test.IntegrationTests;

public class CustomersControllerTests : ApmsBaseTest
{
    public CustomersControllerTests(CustomWebApplicationFactory<Program> factory): base(factory)
    {
    }

    [Fact]
    public async Task CreateAffiliateAndCustomer()
    {
        var affiliateName = TestUtils.RandomString();
        var customerName = TestUtils.RandomString();

        var affiliate = await CreateAffiliate(affiliateName);

        await CreateCustomer(customerName, affiliate.Id);
    }

    [Fact]
    public async Task CreateAffiliateAndCustomerThenList()
    {
        var affiliateName = TestUtils.RandomString();
        var customerName = TestUtils.RandomString();

        var affiliate = await CreateAffiliate(affiliateName);

        var customer = await CreateCustomer(customerName, affiliate.Id);

        var response = await _client.GetAsync($"/api/Customers/GetByAffiliate?affiliateId={affiliate.Id}");

        Assert.NotNull(response);

        response.EnsureSuccessStatusCode();

        var customers = await response.Content.ReadFromJsonAsync<IEnumerable<CustomerDto>>();

        Assert.NotNull(customers);

        Assert.Collection(customers, item => Assert.Equal(customer.Id, item.Id));
    }
}
