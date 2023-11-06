using AffiliateProgramManagementSystem.Data;
using AffiliateProgramManagementSystem.Exceptions;
using AffiliateProgramManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AffiliateProgramManagementSystem.Services;

/// <summary>
/// Interface for basic Affiliate Program Management system functionality.
/// </summary>
public interface IAffiliateProgramService
{
    /// <summary>
    /// Creates a new Affiliate and returns the id.
    /// </summary>
    Task<int> CreateAffiliate(AffiliateDto affiliateDto);
    /// <summary>
    /// Creates a new Customer and returns the id.
    /// </summary>
    Task<int> CreateCustomer(CustomerDto affiliateDto);
    /// <summary>
    /// Returns the list of Customers referred by an Affiliate.
    /// </summary>
    Task<IEnumerable<CustomerDto>> GetCustomerByAffiliate(int affiliateId);
    /// <summary>
    /// Returns the list of all Affiliates
    /// </summary>
    Task<IEnumerable<AffiliateDto>> GetAllAffiliates();
    /// <summary>
    /// Returns a Customer filtering by id.
    /// </summary>
    Task<CustomerDto> GetCustomer(int customerId);
    /// <summary>
    /// Returns the count of referred customers by an Affiliate
    /// </summary>
    Task<int> GetReferredCount(int affiliateId);
}

/// <summary>
/// Concrete implementation of IAffiliateProgramService
/// </summary>
public class AffiliateProgramService : IAffiliateProgramService
{
    private readonly ILogger<AffiliateProgramService> _logger;
    private readonly AffiliateProgramDbContext _dbContext;

    /// <summary>
    /// Constructor
    /// </summary>
    public AffiliateProgramService(ILogger<AffiliateProgramService> logger, AffiliateProgramDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    /// <inheritdoc/>
    public async Task<int> CreateAffiliate(AffiliateDto affiliateDto)
    {
        var affiliate = new Affiliate()
        {
            Name = affiliateDto.Name
        };

        _dbContext.Affiliates.Add(affiliate);

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Affiliate created with id:'{id}'", affiliate.Id);

        return affiliate.Id;
    }

    /// <inheritdoc/>
    public async Task<int> CreateCustomer(CustomerDto customerDto)
    {
        if (!_dbContext.Affiliates.Any(a => a.Id == customerDto.AffiliateId))
            throw new BadRequestException("Invalid AffiliateId");

        var customer = new Customer()
        {
            Name = customerDto.Name,
            AffiliateID = customerDto.AffiliateId
        };

        _dbContext.Customers.Add(customer);

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Customer created with id:'{id}'", customer.Id);

        return customer.Id;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<AffiliateDto>> GetAllAffiliates()
    {
        var affiliates = await _dbContext.Affiliates.Select(a => new AffiliateDto()
        {
            Id = a.Id,
            Name = a.Name
        }).ToListAsync();

        return affiliates;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CustomerDto>> GetCustomerByAffiliate(int affiliateId)
    {
        if (!_dbContext.Affiliates.Any(a => a.Id == affiliateId))
            throw new BadRequestException("Invalid AffiliateId");

        var customers = await _dbContext.Customers
                            .Where(c => c.AffiliateID == affiliateId)
                                .Select(c => new CustomerDto()
                                {
                                    Name = c.Name,
                                    Id = c.Id,
                                    AffiliateId = affiliateId
                                }).ToListAsync();
        return customers;
    }

    /// <inheritdoc/>
    public async Task<CustomerDto> GetCustomer(int customerId)
    {
        var customerDto = default(CustomerDto?);
        var customer = await _dbContext.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == customerId);

        if (customer == null)
            throw new NotFoundException();

        customerDto = new CustomerDto()
        {
            Name = customer.Name,
            Id = customer.Id
        };

        return customerDto;
    }

    /// <inheritdoc/>
    public Task<int> GetReferredCount(int affiliateId)
    {
        if (!_dbContext.Affiliates.Any(a => a.Id == affiliateId))
            throw new BadRequestException("Invalid AffiliateId");

        var referredCount = _dbContext.Customers
                           .Where(c => c.AffiliateID == affiliateId).Count();

        return Task.FromResult(referredCount);
    }
}
