using AffiliateProgramManagementSystem.Data;
using AffiliateProgramManagementSystem.Exceptions;
using AffiliateProgramManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AffiliateProgramManagementSystem.Services;

/// <summary>
/// Interface for basic Affiliate operations.
/// </summary>
public interface IAffiliateService
{
    /// <summary>
    /// Creates a new Affiliate and returns the id.
    /// </summary>
    Task<int> CreateAffiliate(AffiliateDto affiliateDto);
    /// <summary>
    /// Returns the list of all Affiliates
    /// </summary>
    Task<IEnumerable<AffiliateDto>> GetAllAffiliates();
    /// <summary>
    /// Returns the count of referred customers by an Affiliate
    /// </summary>
    Task<int> GetReferredCount(int affiliateId);
}

/// <summary>
/// Concrete implementation of IAffiliateService
/// </summary>
public class AffiliateService : BaseService, IAffiliateService
{
    /// <summary>
    /// Constructor
    /// </summary>
    public AffiliateService(ILogger<AffiliateService> logger, AffiliateProgramDbContext dbContext) : base(logger, dbContext)
    {
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
    public Task<int> GetReferredCount(int affiliateId)
    {
        if (!_dbContext.Affiliates.Any(a => a.Id == affiliateId))
            throw new BadRequestException("Invalid AffiliateId");

        var referredCount = _dbContext.Customers
                           .Where(c => c.AffiliateID == affiliateId).Count();

        return Task.FromResult(referredCount);
    }
}
