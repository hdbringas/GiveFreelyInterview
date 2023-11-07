using AffiliateProgramManagementSystem.Data;
using AffiliateProgramManagementSystem.Exceptions;
using AffiliateProgramManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AffiliateProgramManagementSystem.Services;

/// <summary>
/// Interface for Customer service
/// </summary>
public interface ICustomerService
{
    /// <summary>
    /// Creates a new Customer and returns the id.
    /// </summary>
    Task<int> CreateCustomer(CustomerDto customerDto);
    /// <summary>
    /// Returns a Customer filtering by id.
    /// </summary>
    Task<CustomerDto> GetCustomer(int customerId);
    /// <summary>
    /// Returns the list of Customers referred by an Affiliate.
    /// </summary>
    Task<IEnumerable<CustomerDto>> GetCustomerByAffiliate(int affiliateId);
}

/// <summary>
/// Concrete implementation of ICustomerService
/// </summary>
public class CustomerService : BaseService, ICustomerService
{
    public CustomerService(ILogger<CustomerService> logger, AffiliateProgramDbContext dbContext) : base(logger, dbContext)
    {
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
            Id = customer.Id,
            AffiliateId= customer.AffiliateID
        };

        return customerDto;
    }
}
