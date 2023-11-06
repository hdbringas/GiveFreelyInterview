using AffiliateProgramManagementSystem.Data;
using AffiliateProgramManagementSystem.Test.Utils;
using System.Net.Http.Json;

namespace AffiliateProgramManagementSystem.Test.IntegrationTests;

public class AffiliatesControllerTests : ApmsBaseTest
{
    public AffiliatesControllerTests(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetAffiliates_ReturnsAllAffiliates()
    {
        var response = await _client.GetAsync("/api/Affiliates");

        Assert.NotNull(response);

        response.EnsureSuccessStatusCode();

        var affiliates = await response.Content.ReadFromJsonAsync<IEnumerable<Affiliate>>();

        Assert.NotNull(affiliates);
    }

    [Fact]
    public async Task GetAffiliates_CreateAndListAllAffiliates()
    {
        var affiliate = await CreateAffiliate(TestUtils.RandomString());

        var response = await _client.GetAsync("/api/Affiliates");

        Assert.NotNull(response);

        response.EnsureSuccessStatusCode();

        var affiliates = await response.Content.ReadFromJsonAsync<IEnumerable<Affiliate>>();

        Assert.NotNull(affiliates);

        Assert.Contains(affiliates, item => item.Id == affiliate.Id);
    }

    [Fact]
    public async Task GetReferredCountTest()
    {
        var affiliate = await CreateAffiliate(TestUtils.RandomString());

        await CreateCustomer(TestUtils.RandomString(), affiliate.Id);

        await CreateCustomer(TestUtils.RandomString(), affiliate.Id);

        var response = await _client.GetAsync($"/api/Affiliates/ReferredCount?affiliateId={affiliate.Id}");

        Assert.NotNull(response);

        response.EnsureSuccessStatusCode();

        var referredCount = await response.Content.ReadAsStringAsync();

        Assert.NotNull(referredCount);

        Assert.Equal("2", referredCount);
    }
}