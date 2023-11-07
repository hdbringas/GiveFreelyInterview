using AffiliateProgramManagementSystem.Data;

namespace AffiliateProgramManagementSystem.Services;

/// <summary>
/// Base service that holds a reference to the db context and logger.
/// </summary>
public class BaseService
{
    protected readonly ILogger _logger;
    protected readonly AffiliateProgramDbContext _dbContext;

    /// <summary>
    /// Constructor
    /// </summary>
    protected BaseService(ILogger logger, AffiliateProgramDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
}
