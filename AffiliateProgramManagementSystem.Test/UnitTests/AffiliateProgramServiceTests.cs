using AffiliateProgramManagementSystem.Data;
using AffiliateProgramManagementSystem.Exceptions;
using AffiliateProgramManagementSystem.Models;
using AffiliateProgramManagementSystem.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace AffiliateProgramManagementSystem.Test.UnitTests;

public class AffiliateProgramServiceTests
{
    private readonly ILogger<AffiliateProgramService> _logger;
    private readonly AffiliateProgramDbContext _dbContext;

    public AffiliateProgramServiceTests()
    {
        ServiceCollection services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton<DbConnection>(container =>
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            return connection;
        });

        services.AddDbContext<AffiliateProgramDbContext>((container, options) =>
        {
            var connection = container.GetRequiredService<DbConnection>();
            options.UseSqlite(connection);
        });

        var serviceProvier = services.BuildServiceProvider();
        var factory = serviceProvier.GetRequiredService<ILoggerFactory>();

        _logger = factory.CreateLogger<AffiliateProgramService>();
        _dbContext = serviceProvier.GetRequiredService<AffiliateProgramDbContext>();
        _dbContext.Database.Migrate();
    }

    [Fact]
    public async Task CreateCustomer_BadRequestException()
    {
        var apService = new AffiliateProgramService(_logger, _dbContext);

        await Assert.ThrowsAsync<BadRequestException>(() => apService.CreateCustomer(new CustomerDto() { AffiliateId = -1 }));
    }

    [Fact]
    public async Task GetCustomerByAffiliate_BadRequestException()
    {
        var apService = new AffiliateProgramService(_logger, _dbContext);

        await Assert.ThrowsAsync<BadRequestException>(() => apService.GetCustomerByAffiliate(-1));
    }

    [Fact]
    public async Task GetReferredCount_BadRequestException()
    {
        var apService = new AffiliateProgramService(_logger, _dbContext);

        await Assert.ThrowsAsync<BadRequestException>(() => apService.GetReferredCount(-1));
    }

    [Fact]
    public async Task GetCustomer_BadRequestException()
    {
        var apService = new AffiliateProgramService(_logger, _dbContext);

        await Assert.ThrowsAsync<NotFoundException>(() => apService.GetCustomer(-1));
    }
}
